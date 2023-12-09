# 12주차 Unity의 기초 (RigidBody와 Material을 이용한 중력 구현과 오브젝트 간단 구현)
유니티의 기본적인 오브젝트 생성과 RigidBody 및 Physic Material을 이용한 중력 구현, 게임 오브젝트 안에 간단한 키 입력 스크립트를 통해 구현한 간단한 두가지 예제입니다.
## <구현 사진>
1. 밸런스 게임<br>
![12주차과제-1-](https://github.com/BankBoy22/game/assets/48702307/47e7dbec-ff33-40a3-8c1f-42caa4efb473)<br>
2. Rolling Ball<br>
![12주차과제](https://github.com/BankBoy22/game/assets/48702307/2386ebf1-8d0c-4556-9c30-7395b040b215)
## 1. 밸런스 게임
위 예제에서는 cube와 sphere 오브젝트를 통해 간단한 밸런스 게임을 구현하였습니다.<br>
![PlaneMove코드](https://github.com/BankBoy22/game/assets/48702307/72a9bb25-44ed-44c2-be61-a938a9082cc3)
위의 코드는 키 입력을 통해 바닥 큐브의 회전을 변경하는 코드입니다.<br>
Update()함수 안에 기능을 넣음으로써 매 프레임마다 실행되게끔 하며, Quaternion.AngleAxis(angle, axis) 함수에 키입력값과 축 값을 넣음으로써
회전을 수행합니다.
## 2. Rolling Ball
위 예제에서는 Cube 오브젝트를 적절히 배치하여 Sphere안에 Rigidbody와 Physic Material을 통해 중력을 구현하여 
게임이 실행되면 공이 스스로 중력을 통해 굴러가는 간단한 예제 입니다. 위의 예제는 스크립트를 사용하지 않고 구현 가능합니다.
