#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <time.h>
#include <math.h>
#include <conio.h>
#include <process.h>
#include <windows.h>
#include <mmsystem.h> 

#pragma comment(lib, "winmm.lib")

#define PATTERN_LENGTH 5 // 패턴 길이
#define NUM_ROUNDS 9      // 총 라운드 횟수
#define ROUND_DURATION 10 // 라운드 당 시간 (초)
#define TIMER_INTERVAL 1000 // 타이머 간격 (1초)

char pattern[PATTERN_LENGTH + 1]; // 패턴을 저장하는 배열 (+1은 널 종료 문자 포함)
char userInput[PATTERN_LENGTH + 1]; // 사용자 입력을 저장하는 배열

int timer = ROUND_DURATION; // 타이머 값 초기화

//키보드 값
#define UP 0
#define DOWN 1
#define LEFT 2
#define RIGHT 3
#define SUBMIT 4
#define ESC 5

enum Color { BLACK, BLUE, GREEN, CYAN, RED, MAGENTA, YELLOW, WHITE, BROWN };

void init(void); // 초기화
void time_pass(time_t start); 
void display_time(long nsecond);
void backgroundChange(int color, int bgcolor); // 배경색 변경
void soundPlay(int index); // 음악 재생

//인트로 화면 함수//
void intro(void);						// 게임 시작 전 인트로 화면
void titledraw(void);					// 게임 제목 그리기
int menudraw(void);						// 메뉴 그리기
void infoDraw(void);					// 게임 정보 그리기
void settingDraw(void);					// 게임 설정 그리기 
void gotoxy(int x, int y);				// 커서 이동
int KeyControl(void);					// 키보드 입력 받기

//게임 진행 함수//
void SelectMap();						// 곡 선택
void PlayMap(int index);				// 게임 플레이
void ReadyView(int index);				// 준비 뷰
void generateRandomPattern(void);		//패턴생성함수 
void showPattern(void);					//생성된 패턴을 보여줌 
void updateTimer(void);					//남은 시간을 보여줌
int getUserInput(void);					//사용자의 입력을받음 
 

void updateTimer() {
    printf("남은 시간: %d 초\n", timer);
    Sleep(TIMER_INTERVAL); // 1초 대기
    timer--;
}

void generateRandomPattern() {
	int i;
    for (i = 0; i < PATTERN_LENGTH; i++) {
        // 0~3 사이의 랜덤한 숫자를 생성
        int random = rand() % 4;
        switch (random) {
            case 0:
                pattern[i] = 'w';
                break;
            case 1:
                pattern[i] = 'a';
                break;
            case 2:
                pattern[i] = 's';
                break;
            case 3:
                pattern[i] = 'd';
                break;
        }
    }
    pattern[PATTERN_LENGTH] = '\0'; // 널 종료 문자 추가
}

void showPattern() {
    int i;
    Sleep(1000);
    for (i = 0; i < PATTERN_LENGTH; i++) {
        printf("%c ", pattern[i]);
        Sleep(1000);
        printf("\b\b  \b\b"); // 입력한 문자를 지우기 위해 백스페이스 문자를 출력
    }
    printf("\n");
    printf("패턴을 똑같이 입력하세요\n");
}

int getUserInput() {
    //사용자 입력을 받는다.
    int i;
    for (i = 0; i < PATTERN_LENGTH; i++) {
        char input = getch();
        if (input == 'w' || input == 'a' || input == 's' || input == 'd') {
            userInput[i] = input;
            printf("%c ", userInput[i]);
        } else {
            userInput[i] = '\0';
            return 0;
        }
    }
}
void ClearCursor() // 커서 안뜨게
{ 
	CONSOLE_CURSOR_INFO c = { 0 };
	c.dwSize = 1;
	c.bVisible = FALSE;
	SetConsoleCursorInfo(GetStdHandle(STD_OUTPUT_HANDLE), &c);
}

int main(void)
{
	init(); // 초기화
    intro(); // 인트로 화면 출력
	return 0;
}

void init(void)
{
	//콘솔창 크기 설정
	system("mode con:cols=70 lines=50 | title Rhythm Game"); //가로 100, 세로 100
	PlaySound(TEXT("intro.wav"), NULL, SND_FILENAME | SND_ASYNC | SND_LOOP);
	HANDLE consoleHandle = GetStdHandle(STD_OUTPUT_HANDLE); // 콘솔 핸들
	CONSOLE_CURSOR_INFO ConsoleCursor;
	ConsoleCursor.bVisible = 0;
	ConsoleCursor.dwSize = 1;
	SetConsoleCursorInfo(consoleHandle, &ConsoleCursor); // 커서 없애기
}

void gotoxy(int x, int y)
{
	COORD pos={x,y};
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), pos);
}

void soundPlay(int index){
    //0이면 sound1.wav 실행, 1이면 sound2.wav 실행, 2이면 sound3.wav 실행
    switch(index){
        case 0:
            PlaySound(TEXT("sound1.wav"), NULL, SND_FILENAME | SND_ASYNC);
            break;
        case 1:
            PlaySound(TEXT("sound2.wav"), NULL, SND_FILENAME | SND_ASYNC);
            break;
        case 2:
            PlaySound(TEXT("sound3.wav"), NULL, SND_FILENAME | SND_ASYNC);
            break;
    }
}

void display_time(long nsecond)
{
	printf("<남은시간 %.2ld초>", nsecond);
}

void intro(void) // 게임 시작 전 인트로 화면
{
	while(1){
		titledraw(); // 게임 제목 출력
		int menuCode = menudraw();
		if(menuCode == 0){
			SelectMap(); // 곡 선택
		} else if(menuCode == 1){
			infoDraw();// 게임 정보
		} else if(menuCode == 2){
			// 게임 설정 
			settingDraw(); 
		} else if(menuCode == 3){
			// 게임 종료
			exit(0);
		}
		system("cls"); // 화면 지우기
	}
}

void titledraw(void) // 게임 시작 전 인트로 화면
{
	printf("VER 0.7");
	int x=12, y=2;
	gotoxy(x,y);
	printf(" ____    ______            _______   ");
	gotoxy(x,y+1);
	printf("|  _ \\  |  ____|     /\\   |__   __|  ");
	gotoxy(x,y+2);
	printf("| |_) | | |__       /  \\     | |     ");
	gotoxy(x,y+3);
	printf("|  _ <  |  __|     / /\\ \\    | |     ");
	gotoxy(x,y+4);
	printf("| |_) | | |____   / ____ \\   | |     ");
	gotoxy(x,y+5);
	printf("|____/  |______| /_/    \\_\\  |_|     \n");
	gotoxy(x,y+6);
	printf("                                        \n");
}

int menudraw(void) // 게임 시작 전 인트로 화면
{
	int x = 24, y = 12; // 메뉴의 x, y 좌표
	// 이동과 선택 키 설명
	gotoxy(x-4, y+6);
	printf("이동 : W, A, S, D");
	gotoxy(x-4, y+7);
	printf("선택 : 스페이스바");
	gotoxy(x-2, y); // -2를 해주는 이유는 메뉴 앞에 ">"를 출력하기 위해서
	printf("> 게임시작");
	gotoxy(x, y+1);
	printf("게임정보");
	gotoxy(x, y+2);
	printf("게임설정");
	gotoxy(x, y+3);
	printf("게임종료");
	while(1) // 무한반복
	{
		int n = KeyControl(); // 키보드 값 받기
		switch (n)
		{
			case UP: {
				Beep(500, 200);
				if(y > 12)
				{
					gotoxy(24-2, y);
					printf(" ");
					gotoxy(24-2, --y);
					printf(">");
				}
				break;
			}

			case DOWN: {
				Beep(500, 200);
				if(y < 15)
				{
					gotoxy(x-2, y);
					printf(" ");
					gotoxy(x-2, ++y);
					printf(">");
				}
				break;
			}

			case SUBMIT: {
				Beep(410, 200);
				return y-12;
			}
		}
	}
}

void infoDraw(){
	system("cls");
	printf("\n\n");
	printf("                   [ 조작법 ]\n\n");
	printf("               이동 : W,A,S,D\n");
	printf("               선택 : 스페이스바\n\n\n\n\n\n");
	printf("           개발자 : 2019975070 한재훈\n\n");
	printf("           github : https://github.com/BankBoy22\n\n\n");
	printf("           ESC를 누르면 메인화면으로 이동합니다.");

	while(1){
		if(KeyControl() == ESC)
			Beep(260, 200);
			break;
	}
}

void SelectMap() // 곡 선택
{
	//현재 재생되있는 음악 정지
    PlaySound(NULL, NULL, SND_FILENAME | SND_ASYNC | SND_LOOP);
    FILE *file = fopen("SongList.txt", "r"); // 'SongList.txt'라는 파일에서 읽기 모드로 연다.
    if (file == NULL) { // 파일 열기 실패 시 에러 메시지 출력 후 함수 종료
        printf("Could not open song list.");
        return;
    }

    char line[256]; // 한 줄씩 읽어올 문자열 버퍼
    int total_lines = 0; // 총 라인 수
    while (fgets(line, sizeof(line), file)) {   // 한 줄씩 읽어서 출력한다.
		total_lines++;
	}
    int index = 0;  // 현재 선택된 노래 인덱스
	soundPlay(index); // 음악 재생
	
    while (1) {
        system("cls"); // 화면 지우기
        gotoxy(20, 1);
        printf("[ Song Selection ]\n");
        gotoxy(20, 18);
		printf("곡 이동 : W, S");
		gotoxy(20, 19);
		printf("선택 : 스페이스바");

        rewind(file);  // 파일 포인터를 처음으로 돌린다.

        int i = 0;
        while (fgets(line, sizeof(line), file)) {   // 한 줄씩 읽어서 출력한다.
            int len = strlen(line);
            if (line[len - 1] == ' ') { 
                line[len - 1] = '\0'; 
            }
            
            gotoxy(10, i + 5);
            
            if (i == index) {   // 현재 선택된 노래 앞에 '>' 문자 표시
                printf("> ");
            } else {
                printf("  ");
            }
            
            printf("%s", line); 
            
            i++;
        }
		
		int prevIndex = index;	// 이전 인덱스 저장
		
        int key = KeyControl();
        
		switch(key){
			case UP:
				Beep(500, 200);
				if(index > 0){
					index--;
				}
				soundPlay(index); // 음악 재생		
				break;
			case DOWN:
				Beep(500, 200);
				if(index < total_lines -1) {
					index++;
				}
				soundPlay(index); // 음악 재생		
				break;
			case SUBMIT:
				Beep(410, 200);
				PlaySound(NULL, NULL, SND_FILENAME | SND_ASYNC | SND_LOOP);
				ReadyView(index);
				break;
			case ESC:
				Beep(260, 200);
				fclose(file);
				PlaySound(TEXT("intro.wav"), NULL, SND_FILENAME | SND_ASYNC | SND_LOOP);
				return; 
		}
    }

}

void settingDraw(){
	system("cls"); // 화면 지우기
    int x =15, y = 6;
    gotoxy(20, 2);
    printf("[ 게임 설정 ]\n");
    gotoxy(x-5, y+8);
    printf("바꾸고 싶은 테마의 번호를 선택한뒤 스페이스바를 누르시오.");
    //설정 메뉴 구성   
    gotoxy(x, y-1);
    printf("<테마 변경>");
    gotoxy(x-2, y);
    printf("> 1. 공포 테마(배경색: 검정, 글자색: 빨강)");
    gotoxy(x, y+1);
    printf("2. 봄 테마(배경색: 하늘색, 글자색: 노랑)");
    gotoxy(x, y+2);
    printf("3. 가을 테마(배경색: 갈색, 글자색: 노랑)");
    gotoxy(x, y+3);
    printf("4. 겨울 테마(배경색: 하얀색, 글자색: 파란색)");
    gotoxy(x, y+4);
    printf("5. 기본 테마(배경색: 검정, 글자색: 하양)"); 
	while(1){
		
        int key = KeyControl();

        switch(key){
            case UP:
            	Beep(500, 200);
                if(y > 6){
                    gotoxy(x-2, y);
                    printf(" ");
                    gotoxy(x-2, --y);
                    printf(">");
                }
                break;
            case DOWN:
            	Beep(500, 200);
                if(y < 10){
                    gotoxy(x-2, y);
                    printf(" ");
                    gotoxy(x-2, ++y);
                    printf(">");
                }
                break;
            case SUBMIT:
            	Beep(410, 200);
                switch(y){
                    case 6:
                        backgroundChange(RED, BLACK);
                        return;
                    case 7:
                        backgroundChange(YELLOW, CYAN);
                        return;
                    case 8:
                        backgroundChange(YELLOW, BROWN);
                        return;
                    case 9:
                        backgroundChange(BLUE, WHITE);
                        return;
                    case 10:
                        backgroundChange(WHITE, BLACK);
                        return;
                }
                break;
            case ESC:
            	Beep(260, 200);
                return;
        }    
	}	
}

void backgroundChange(int color, int bgcolor){
   	color &= 0xf;
	bgcolor &= 0xf;
    SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (bgcolor << 4) | color);
}

void ReadyView(int index){         //게임 스타트! 

	while(1){
		//3초 뒤에 시작합니다. 안내
        system("cls");
        gotoxy(20, 2);
        printf("[ 게임 시작 ]\n");
        Sleep(1000);
        gotoxy(17, 5);
        Beep(500, 200);
        printf("3초 뒤에 시작합니다.");
        Sleep(1000);
        gotoxy(17, 6);
        Beep(500, 200);
        printf("2초 뒤에 시작합니다.");
        Sleep(1000);
        gotoxy(17, 7);
        Beep(500, 200);
        printf("1초 뒤에 시작합니다.");
        Sleep(1000);
        gotoxy(24, 10);
        Beep(1000, 200);
        printf("시작!");
        Sleep(1000);
        PlayMap(index);
	}	
}

void PlayMap(int index){
    //현재 재생되있는 음악 정지
    PlaySound(NULL, NULL, SND_FILENAME | SND_ASYNC | SND_LOOP);
    //선택된 곡 재생
    soundPlay(index);
    int x=24,y=4;
    while(1){
        //게임 화면 출력
        system("cls");
        gotoxy(20, 2);
        printf("[ 게임 플레이 ]\n\n\n");
        int score = 0;
        gotoxy(0, 4);
        int round;
        for (round = 1; round <= NUM_ROUNDS; round++) {
            printf("\nRound %d 시작!\n\n", round);
            generateRandomPattern();
            showPattern();

            timer = ROUND_DURATION;
			
            while (timer > 0) {
                updateTimer();
                printf("\n사용자 입력 => ");

                if (getUserInput()) {
                    if (strcmp(pattern, userInput) == 0) {
                        printf("\n정답!\n");
                        score++;
                    } else {
                        printf("\n오답. 정답은: %s\n", pattern);
                    }
                    break;
                }
            }
        
        }
        printf("게임 종료! 점수: %d\n", score);
        printf("다시 하시겠습니까? (y/n)");
        char input = getch();
        while(1){
            if(input == 'y' || input == 'n'){
                break;
            }
        }
        if (input == 'y') {
            continue;
        } else {
            //프로그램이 종료됩니다 문구 후 2초뒤 종료
            printf("\n프로그램이 종료됩니다.");
            Sleep(2000);
            exit(0);
        }
        
    }
}

int KeyControl()
{
	char temp = getch();
	if(temp == 'w' || temp == 'W')
	{
		return UP;
	}
	else if(temp == 's' || temp == 'S')
	{
		return DOWN;
	}
	else if(temp == 'a' || temp == 'A')
	{
		return LEFT;
	}
	else if(temp == 'd' || temp == 'D')
	{
		return RIGHT;
	}
	else if(temp == ' ') 
	{
		return SUBMIT;
	}
	else if(temp == 27) // ESC
	{
	    return ESC;
    }
}

