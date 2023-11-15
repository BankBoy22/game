//사운드 재생
var sound = new Howl({
  src: ['sound/ingame.mp3']
});

var jumpSound = new Howl({
  src: ['sound/jump.mp3']
});

var itemSound = new Howl({
  src: ['sound/item.mp3']
});

sound.play();

//점수 변수
var score = 0;
//적의 HP 변수
var enemyHP = 3;

//난이도
const storedDifficulty = localStorage.getItem("difficulty");

// Cannon.js world
const world = new CANNON.World();
world.gravity.set(0, -9.8, 0); // 중력을 설정

// Three.js scene
const scene = new THREE.Scene();
scene.background = new THREE.Color(0x004fff); // Hex 코드로 하늘색을 나타내는 값

// Three.js renderer
const renderer = new THREE.WebGLRenderer({ canvas: document.getElementById("gameCanvas") });
renderer.setSize(window.innerWidth, window.innerHeight); 
renderer.shadowMap.enabled = true; // 그림자를 만들도록 설정
document.body.appendChild(renderer.domElement);

//캔버스 위에 점수를 표시할 HTML 요소를 추가
const scoreElement = document.createElement("div");
scoreElement.style.position = "absolute";
scoreElement.style.fontWeight = "bold";
scoreElement.style.top = "20px";
scoreElement.style.left = "15px";
scoreElement.style.color = "black";
scoreElement.style.fontSize = "30px";
scoreElement.innerHTML = `Score: ${score}`;
document.body.appendChild(scoreElement);

//중간 맨위에 난이도를 표시할 HTML 요소를 추가
const difficultyElement = document.createElement("div");
difficultyElement.style.position = "absolute";
difficultyElement.style.fontWeight = "bold";
difficultyElement.style.top = "20px";
difficultyElement.style.left = "50%";
difficultyElement.style.color = "black";
difficultyElement.style.fontSize = "30px";
difficultyElement.innerHTML = `난이도: 쉬움`;
document.body.appendChild(difficultyElement);

// 점수를 갱신하는 함수
function updateScore() {
  score += 1;
  scoreElement.innerHTML = `Score: ${score}`;
}

// Three.js camera
const camera = new THREE.PerspectiveCamera(60, window.innerWidth / window.innerHeight, 0.1, 1000);
camera.position.set(0, 10, 10);
camera.lookAt(new THREE.Vector3(0, 0, 0));

// Skybox
const skyMaterialArray = [];
const texture_ft = new THREE.TextureLoader().load('skybox/posx.jpg');
const texture_bk = new THREE.TextureLoader().load('skybox/negx.jpg');
const texture_up = new THREE.TextureLoader().load('skybox/posy.jpg');
const texture_dn = new THREE.TextureLoader().load('skybox/negy.jpg');
const texture_rt = new THREE.TextureLoader().load('skybox/posz.jpg');
const texture_lf = new THREE.TextureLoader().load('skybox/negz.jpg');
skyMaterialArray.push(new THREE.MeshBasicMaterial({ map: texture_ft }));
skyMaterialArray.push(new THREE.MeshBasicMaterial({ map: texture_bk }));
skyMaterialArray.push(new THREE.MeshBasicMaterial({ map: texture_up }));
skyMaterialArray.push(new THREE.MeshBasicMaterial({ map: texture_dn }));
skyMaterialArray.push(new THREE.MeshBasicMaterial({ map: texture_rt }));
skyMaterialArray.push(new THREE.MeshBasicMaterial({ map: texture_lf }));
for (let i = 0; i < 6; i++)
  skyMaterialArray[i].side = THREE.BackSide;
const skyboxGeo = new THREE.BoxGeometry(1000, 1000, 1000);
const skybox = new THREE.Mesh(skyboxGeo, skyMaterialArray);
skybox.position.set(0, 0, 0);
scene.add(skybox);

// Directional light
const directionalLight = new THREE.DirectionalLight(0xffffff, 1); 
directionalLight.position.set(-100, 100, 100);
directionalLight.target.position.set(0, 0, 0);
directionalLight.castShadow = true;
directionalLight.shadow.bias = -0.001;
directionalLight.shadow.mapSize.width = 4096;  // 그림자 맵의 너비
directionalLight.shadow.mapSize.height = 4096; // 그림자 맵의 높이
directionalLight.shadow.camera.near = 0.5;     // 그림자 카메라의 가까운 범위
directionalLight.shadow.camera.far = 500;       // 그림자 카메라의 먼 범위
directionalLight.shadow.camera.left = 50;     // 그림자 카메라의 왼쪽 범위
directionalLight.shadow.camera.right = -50;     // 그림자 카메라의 오른쪽 범위
directionalLight.shadow.camera.top = 50;       // 그림자 카메라의 위쪽 범위
directionalLight.shadow.camera.bottom = -50;   // 그림자 카메라의 아래쪽 범위
scene.add(directionalLight);

// Ambient light
const ambientLight = new THREE.AmbientLight(0xFFFFFF, 0.25);
scene.add(ambientLight);

// 필드 Cannon.js ground
const groundShape = new CANNON.Box(new CANNON.Vec3(50, 0.1, 50));
const groundBody = new CANNON.Body({ mass: 0 });
groundBody.addShape(groundShape);
world.addBody(groundBody);

// 필드 Three.js ground
// 필드 텍스처 추가
const groundTexture = new THREE.TextureLoader().load("skybox/ground.jpg");
groundTexture.wrapS = THREE.RepeatWrapping;
groundTexture.wrapT = THREE.RepeatWrapping;
groundTexture.repeat.set(100, 100);
groundTexture.anisotropy = 16;//텍스처의 선명도를 설정
const groundGeometry = new THREE.BoxGeometry(100, 0.1, 100); 
const groundMaterial = new THREE.MeshStandardMaterial({ map : groundTexture, roughness: 0.5, metalness: 0.5 });
const ground = new THREE.Mesh(groundGeometry, groundMaterial);
ground.receiveShadow = true;
scene.add(ground);

// 플레이어 Cannon.js ball
const ballShape = new CANNON.Sphere(0.5);
const ballBody = new CANNON.Body({ mass: 1 });
ballBody.addShape(ballShape);
ballBody.position.set(0, 1, 0);
world.addBody(ballBody);

// 플레이어 Three.js ball
const ballGeometry = new THREE.SphereGeometry(0.5);
const ballMaterial = new THREE.MeshStandardMaterial({ color: 0xff0000, roughness: 0.5, metalness: 1 });
const ball = new THREE.Mesh(ballGeometry, ballMaterial);
ball.castShadow = true;
scene.add(ball);

var bullets = []; // 총알 배열 생성


// 플레이어의 공격 함수
function attack() {
  const bulletGeometry = new THREE.SphereGeometry(0.1);
  const bulletMaterial = new THREE.MeshStandardMaterial({ color: 0xffff00 });
  const bullet = new THREE.Mesh(bulletGeometry, bulletMaterial);
  bullet.position.copy(ball.position);

  const bulletVelocity = new CANNON.Vec3(0, 0, -3); // 총알의 초기 속도
  bullets.push({ mesh: bullet, velocity: bulletVelocity });
  scene.add(bullet);

  const bulletShape = new CANNON.Sphere(0.1);
  const bulletBody = new CANNON.Body({ mass: 0.01 });
  bulletBody.addShape(bulletShape);
  bulletBody.position.copy(ball.position);
  bulletBody.velocity.copy(bulletVelocity);
  world.addBody(bulletBody);
}

// 적 배열 생성
const enemies = [];

// 적 생성 함수 ground의 필드 내에서 랜덤한 위치에서 적을 생성
function createEnemy() {
  const x = Math.random() * 40 - 20;
  const z = Math.random() * 40 - 20;
  const enemy = createCylinder(scene, 1, 1, 2, x, 1, z, 0xff0000);
  enemies.push(enemy);
}

//난이도에 따라 적 생성
if (storedDifficulty == "easy") {
  setInterval(createEnemy, 3000);
  difficultyElement.innerHTML = `난이도: 쉬움`; 
} else if (storedDifficulty == "normal") {
  setInterval(createEnemy, 1000);
  difficultyElement.innerHTML = `난이도: 보통`;
} else if (storedDifficulty == "hard") {
  setInterval(createEnemy, 500);
  difficultyElement.innerHTML = `난이도: 어려움`;
} 

//적은 플레이어에게 다가온다
function updateEnemies() {
  enemies.forEach((enemy) => {
    const enemyPosition = enemy.position;
    const playerPosition = ball.position;

    const direction = new THREE.Vector3();
    direction.subVectors(playerPosition, enemyPosition).normalize();

    //난이도에 따라 적의 속도가 달라진다.
    if (storedDifficulty == "easy") {
      enemyPosition.addScaledVector(direction, 0.1); 
    } else if (storedDifficulty == "normal") {
      enemyPosition.addScaledVector(direction, 0.3);
    } else if (storedDifficulty == "hard") {
    enemyPosition.addScaledVector(direction, 0.5);
    }
  });
}

// 적을 갱신
setInterval(updateEnemies, 100);

// 충돌 감지
function checkCollisions() {
  const ballPosition = ballBody.position;
  enemies.forEach((enemy) => {
    const enemyPosition = enemy.position;
    const distance = ballPosition.distanceTo(enemyPosition);
    if (distance < 1) {
      // 충돌이 감지되면 게임 오버 그리고 점수의 값을 Gameover.html로 넘겨준다.
      localStorage.setItem("score", score);
      gameOver();
    }
  });
}

const items = []; // 아이템 배열 생성

// 새로운 아이템 생성 함수
function createItem(x, y, z) {
  const itemGeometry = new THREE.BoxGeometry(1, 1, 1);
  const itemMaterial = new THREE.MeshStandardMaterial({ color: 0x00ff00 });
  const item = new THREE.Mesh(itemGeometry, itemMaterial);
  item.position.set(x, y, z);

  // 회전 속성 추가
  item.rotationSpeed = new THREE.Vector3(
    Math.random() * 0.1,
    Math.random() * 0.1,
    Math.random() * 0.1
  );

  // 소재에 발광 속성 추가
  itemMaterial.emissive = new THREE.Color(0x00ff00);

  // 나중에 사용할 원래의 발광 색상 저장
  itemMaterial.originalEmissive = itemMaterial.emissive.clone();

  // 아이템에 깜박거림 효과 추가
  itemMaterial.blink = function () {
    const time = performance.now() * 0.001; // 밀리초를 초로 변환
    const blink = Math.sin(time * 5) * 0.5 + 0.5; // 깜박거림 속도와 강도 조절

    // 깜박거림에 기반하여 발광 색상 업데이트
    itemMaterial.emissive.copy(itemMaterial.originalEmissive).multiplyScalar(blink);
  };

  // 아이템을 scene에 추가
  scene.add(item);

  // 아이템의 속성 설정 (예: 점수 증가량)
  item.scoreValue = 2000;

  // 아이템 배열에 추가
  items.push(item);
}

// 주기적으로 아이템 생성
function spawnItem() {
  const x = Math.random() * 40 - 20;
  const z = Math.random() * 40 - 20;
  createItem(x, 1, z);
}

// 아이템 회전 갱신 함수
function updateItemsRotation() {
  items.forEach((item) => {
    // 아이템을 회전
    item.rotation.x += item.rotationSpeed.x;
    item.rotation.y += item.rotationSpeed.y;
    item.rotation.z += item.rotationSpeed.z;

    // 아이템 깜박거림
    item.material.blink();
  });
}

// 주기적으로 아이템 생성
setInterval(spawnItem, 5000);
// 주기적으로 아이템 회전 갱신
setInterval(updateItemsRotation, 50); 

// 아이템과 플레이어의 충돌 감지 함수
function checkItemCollisions() {
  const ballPosition = ballBody.position;
  items.forEach((item, index) => {
    const itemPosition = item.position;
    const distance = ballPosition.distanceTo(itemPosition);
    if (distance < 1) {
      // 아이템과 충돌 시 점수 증가 및 아이템 제거
      score += item.scoreValue;
      scoreElement.innerHTML = `Score: ${score}`;
      // 아이템 먹었을 때의 특별한 효과 (예: 크기 변경)
      item.scale.set(0, 0, 0); // 아이템 크기를 0으로 설정
      itemSound.play();
      scene.remove(item); 
      items.splice(index, 1);
    }
  });
}

//게임 오버 함수
function gameOver() {
  //Gameover.html로 이동
  location.href = "Gameover.html";
}

//일정 구간 떨어지면 게임 오버
function checkGameOver() {
  const ballPosition = ballBody.position;
  if (ballPosition.y < -10) {
    // 충돌이 감지되면 게임 오버 그리고 점수의 값을 Gameover.html로 넘겨준다.
    localStorage.setItem("score", score);
    gameOver();
  }
}

//게임 오버 감지
setInterval(checkGameOver, 100);

// 이동 및 점프 관련 변수
const speed = 5;
const jumpSpeed = 8;
const ballVelocity = new CANNON.Vec3();
let isGrounded = false;
const keyboard = {};


// Animation
function animate() {
  requestAnimationFrame(animate);

  handleKeyboardInput();

  world.step(1 / 60);

  updateScore();// 점수 갱신

  checkCollisions(); // 충돌 감지

  checkItemCollisions(); // 아이템 충돌 감지

  // Ball 위치를 기반으로 카메라 위치를 설정
  camera.position.copy(ball.position).add(new THREE.Vector3(0, 5, -10));

  // Ball 위치를 기반으로 카메라를 바라보게 설정
  camera.lookAt(ball.position);

  renderer.render(scene, camera);
}

// Handle window resize
function onWindowResize() {
  camera.aspect = window.innerWidth / window.innerHeight;
  camera.updateProjectionMatrix();
  renderer.setSize(window.innerWidth, window.innerHeight);
}

window.addEventListener('resize', onWindowResize);

// Start animation
animate();

// 핸들 키보드 입력
window.addEventListener("keydown", (event) => {
  keyboard[event.key] = true;
  // a를 누르면 공격
  if (event.key === "a") {
    attack();
    
  }
});

window.addEventListener("keyup", (event) => {
  keyboard[event.key] = false;
});

// 키보드 입력을 처리하는 함수
function handleKeyboardInput() {
  const ballVelocity = ballBody.velocity;
  ballVelocity.set(0, ballVelocity.y, 0);

  if (keyboard["ArrowLeft"]) {
    ballVelocity.x = speed;
  }
  if (keyboard["ArrowRight"]) {
    ballVelocity.x = -speed;
  }
  if (keyboard["ArrowUp"]) {
    ballVelocity.z = speed;
  }
  if (keyboard["ArrowDown"]) {
    ballVelocity.z = -speed;
  }

  if (keyboard[" "] && isGrounded) {
    ballVelocity.y = jumpSpeed; 
    isGrounded = false;
    jumpSound.play();
    console.log("Ground contact detected");
  } else {
    ballVelocity.y = ballBody.velocity.y;
    console.log("No ground contact");
  }

  if (keyboard["Shift"]) {
    //플레이어의 스피드를 1.5배로 증가
    ballVelocity.x *= 1.5;
    ballVelocity.z *= 1.5;
  }

  // 플레이어의 속도를 갱신
  isGrounded = ballBody.position.y <= 1.5;

  ball.position.copy(ballBody.position);

}

// 총알을 갱신하는 함수
function updateBullets() {
  bullets.forEach((bullet) => {
    bullet.mesh.position.copy(bullet.body.position);
  });
}

setInterval(updateBullets, 100);

