//바닥의 크기와 위치 재질을 인자로 받아 바닥을 생성하는 함수
function createFloor(scene, width, height, x, y, z, color) {
  const floorShape = new CANNON.Plane();
  const floorBody = new CANNON.Body({ mass: 0 });
  floorBody.addShape(floorShape);
  floorBody.position.set(x, y, z);
  world.addBody(floorBody);

  const geometry = new THREE.PlaneGeometry(width, height);
  const material = new THREE.MeshPhongMaterial({ color });
  const plane = new THREE.Mesh(geometry, material);
  plane.rotation.x = -Math.PI / 2; 
  plane.position.set(x, y, z);
  scene.add(plane);
  return plane; // 생성한 바닥 객체를 반환
}

//공의 크기와 위치 재질을 인자로 받아 공을 생성하는 함수
function createBall(scene, radius, x, y, z, color) {
  const ballShape = new CANNON.Sphere(radius);
  const ballBody = new CANNON.Body({ mass: 1 });
  ballBody.addShape(ballShape);
  ballBody.position.set(x, y, z);
  world.addBody(ballBody);
  
  const geometry = new THREE.SphereGeometry(radius, 32, 32);
  const material = new THREE.MeshPhongMaterial({ color });
  const ball = new THREE.Mesh(geometry, material);
  ball.position.set(x, y, z);
  scene.add(ball);
  return ball; // 생성한 공 객체를 반환
}

//상자의 크기와 위치 재질을 인자로 받아 상자를 생성하는 함수
function createBox(scene, width, height, depth, x, y, z, color) {
  const boxShape = new CANNON.Box(new CANNON.Vec3(width / 2, height / 2, depth / 2));
  const boxBody = new CANNON.Body({ mass: 1 });
  boxBody.addShape(boxShape);
  boxBody.position.set(x, y, z);
  world.addBody(boxBody);

  const geometry = new THREE.BoxGeometry(width, height, depth);
  const material = new THREE.MeshPhongMaterial({ color });
  const box = new THREE.Mesh(geometry, material);
  box.position.set(x, y, z);
  scene.add(box);
  return box; // 생성한 상자 객체를 반환
}

//원통의 크기와 위치 재질을 인자로 받아 원통을 생성하는 함수,적으로 만들꺼임
function createCylinder(scene, radiusTop, radiusBottom, height, x, y, z, color) {
  const cylinderShape = new CANNON.Cylinder(radiusTop, radiusBottom, height, 32);
  const cylinderBody = new CANNON.Body({ mass: 1 });
  cylinderBody.addShape(cylinderShape);
  cylinderBody.position.set(x, y, z);
  world.addBody(cylinderBody);
  
  const geometry = new THREE.CylinderGeometry(radiusTop, radiusBottom, height);
  const material = new THREE.MeshPhongMaterial({ color });
  const cylinder = new THREE.Mesh(geometry, material);
  cylinder.position.set(x, y, z);
  cylinder.castShadow = true;
  scene.add(cylinder);
  return cylinder; // 생성한 원통 객체를 반환
}

//빛의 색상과 위치를 인자로 받아 빛을 생성하는 함수
function createLight(scene, color, x, y, z) {
  const light = new THREE.PointLight(color);
  light.position.set(x, y, z);
  scene.add(light);
  return light; // 생성한 빛 객체를 반환
}

//2D 텍스처 이미지의 경로를 인자로 받아 텍스처를 생성하는 함수
function createTexture(src) {
  const texture = new THREE.TextureLoader().load(src);
  return texture; // 생성한 텍스처 객체를 반환
}

function createModel(scene, src, scale, rotation, restitution, friction, x, y, z, callback) {
  const loader = new THREE.GLTFLoader();
  loader.load(src, (gltf) => {
    const model = gltf.scene;
    model.scale.set(scale, scale, scale);
    model.rotation.set(rotation.x, rotation.y, rotation.z);
    model.position.set(x, y, z);
    model.traverse((child) => {
      if (child.isMesh) {
        child.material = new THREE.MeshPhysicalMaterial({ restitution, friction });
      }
    });
    scene.add(model);

    // 모델이 로드되면 콜백을 호출
    if (callback) {
      callback(model);
    }
  });
}

//텍스트와 폰트 크기를 인자로 받아 텍스트를 생성하는 함수
function createText(text, size) {
  const geometry = new THREE.TextGeometry(text, {
    font: new THREE.Font(font), 
    size: size,
    height: 0.1,
    curveSegments: 12,
    bevelEnabled: false, 
  });
  const material = new THREE.MeshPhongMaterial({ color: 0x000000 });
  const textMesh = new THREE.Mesh(geometry, material);
  return textMesh; // 생성한 텍스트 객체를 반환
}



