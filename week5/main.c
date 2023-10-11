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

#define PATTERN_LENGTH 5 // ���� ����
#define NUM_ROUNDS 9      // �� ���� Ƚ��
#define ROUND_DURATION 10 // ���� �� �ð� (��)
#define TIMER_INTERVAL 1000 // Ÿ�̸� ���� (1��)

char pattern[PATTERN_LENGTH + 1]; // ������ �����ϴ� �迭 (+1�� �� ���� ���� ����)
char userInput[PATTERN_LENGTH + 1]; // ����� �Է��� �����ϴ� �迭

int timer = ROUND_DURATION; // Ÿ�̸� �� �ʱ�ȭ

//Ű���� ��
#define UP 0
#define DOWN 1
#define LEFT 2
#define RIGHT 3
#define SUBMIT 4
#define ESC 5

enum Color { BLACK, BLUE, GREEN, CYAN, RED, MAGENTA, YELLOW, WHITE, BROWN };

void init(void); // �ʱ�ȭ
void time_pass(time_t start); 
void display_time(long nsecond);
void backgroundChange(int color, int bgcolor); // ���� ����
void soundPlay(int index); // ���� ���

//��Ʈ�� ȭ�� �Լ�//
void intro(void);						// ���� ���� �� ��Ʈ�� ȭ��
void titledraw(void);					// ���� ���� �׸���
int menudraw(void);						// �޴� �׸���
void infoDraw(void);					// ���� ���� �׸���
void settingDraw(void);					// ���� ���� �׸��� 
void gotoxy(int x, int y);				// Ŀ�� �̵�
int KeyControl(void);					// Ű���� �Է� �ޱ�

//���� ���� �Լ�//
void SelectMap();						// �� ����
void PlayMap(int index);				// ���� �÷���
void ReadyView(int index);				// �غ� ��
void generateRandomPattern(void);		//���ϻ����Լ� 
void showPattern(void);					//������ ������ ������ 
void updateTimer(void);					//���� �ð��� ������
int getUserInput(void);					//������� �Է������� 
 

void updateTimer() {
    printf("���� �ð�: %d ��\n", timer);
    Sleep(TIMER_INTERVAL); // 1�� ���
    timer--;
}

void generateRandomPattern() {
	int i;
    for (i = 0; i < PATTERN_LENGTH; i++) {
        // 0~3 ������ ������ ���ڸ� ����
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
    pattern[PATTERN_LENGTH] = '\0'; // �� ���� ���� �߰�
}

void showPattern() {
    int i;
    Sleep(1000);
    for (i = 0; i < PATTERN_LENGTH; i++) {
        printf("%c ", pattern[i]);
        Sleep(1000);
        printf("\b\b  \b\b"); // �Է��� ���ڸ� ����� ���� �齺���̽� ���ڸ� ���
    }
    printf("\n");
    printf("������ �Ȱ��� �Է��ϼ���\n");
}

int getUserInput() {
    //����� �Է��� �޴´�.
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
void ClearCursor() // Ŀ�� �ȶ߰�
{ 
	CONSOLE_CURSOR_INFO c = { 0 };
	c.dwSize = 1;
	c.bVisible = FALSE;
	SetConsoleCursorInfo(GetStdHandle(STD_OUTPUT_HANDLE), &c);
}

int main(void)
{
	init(); // �ʱ�ȭ
    intro(); // ��Ʈ�� ȭ�� ���
	return 0;
}

void init(void)
{
	//�ܼ�â ũ�� ����
	system("mode con:cols=70 lines=50 | title Rhythm Game"); //���� 100, ���� 100
	PlaySound(TEXT("intro.wav"), NULL, SND_FILENAME | SND_ASYNC | SND_LOOP);
	HANDLE consoleHandle = GetStdHandle(STD_OUTPUT_HANDLE); // �ܼ� �ڵ�
	CONSOLE_CURSOR_INFO ConsoleCursor;
	ConsoleCursor.bVisible = 0;
	ConsoleCursor.dwSize = 1;
	SetConsoleCursorInfo(consoleHandle, &ConsoleCursor); // Ŀ�� ���ֱ�
}

void gotoxy(int x, int y)
{
	COORD pos={x,y};
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), pos);
}

void soundPlay(int index){
    //0�̸� sound1.wav ����, 1�̸� sound2.wav ����, 2�̸� sound3.wav ����
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
	printf("<�����ð� %.2ld��>", nsecond);
}

void intro(void) // ���� ���� �� ��Ʈ�� ȭ��
{
	while(1){
		titledraw(); // ���� ���� ���
		int menuCode = menudraw();
		if(menuCode == 0){
			SelectMap(); // �� ����
		} else if(menuCode == 1){
			infoDraw();// ���� ����
		} else if(menuCode == 2){
			// ���� ���� 
			settingDraw(); 
		} else if(menuCode == 3){
			// ���� ����
			exit(0);
		}
		system("cls"); // ȭ�� �����
	}
}

void titledraw(void) // ���� ���� �� ��Ʈ�� ȭ��
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

int menudraw(void) // ���� ���� �� ��Ʈ�� ȭ��
{
	int x = 24, y = 12; // �޴��� x, y ��ǥ
	// �̵��� ���� Ű ����
	gotoxy(x-4, y+6);
	printf("�̵� : W, A, S, D");
	gotoxy(x-4, y+7);
	printf("���� : �����̽���");
	gotoxy(x-2, y); // -2�� ���ִ� ������ �޴� �տ� ">"�� ����ϱ� ���ؼ�
	printf("> ���ӽ���");
	gotoxy(x, y+1);
	printf("��������");
	gotoxy(x, y+2);
	printf("���Ӽ���");
	gotoxy(x, y+3);
	printf("��������");
	while(1) // ���ѹݺ�
	{
		int n = KeyControl(); // Ű���� �� �ޱ�
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
	printf("                   [ ���۹� ]\n\n");
	printf("               �̵� : W,A,S,D\n");
	printf("               ���� : �����̽���\n\n\n\n\n\n");
	printf("           ������ : 2019975070 ������\n\n");
	printf("           github : https://github.com/BankBoy22\n\n\n");
	printf("           ESC�� ������ ����ȭ������ �̵��մϴ�.");

	while(1){
		if(KeyControl() == ESC)
			Beep(260, 200);
			break;
	}
}

void SelectMap() // �� ����
{
	//���� ������ִ� ���� ����
    PlaySound(NULL, NULL, SND_FILENAME | SND_ASYNC | SND_LOOP);
    FILE *file = fopen("SongList.txt", "r"); // 'SongList.txt'��� ���Ͽ��� �б� ���� ����.
    if (file == NULL) { // ���� ���� ���� �� ���� �޽��� ��� �� �Լ� ����
        printf("Could not open song list.");
        return;
    }

    char line[256]; // �� �پ� �о�� ���ڿ� ����
    int total_lines = 0; // �� ���� ��
    while (fgets(line, sizeof(line), file)) {   // �� �پ� �о ����Ѵ�.
		total_lines++;
	}
    int index = 0;  // ���� ���õ� �뷡 �ε���
	soundPlay(index); // ���� ���
	
    while (1) {
        system("cls"); // ȭ�� �����
        gotoxy(20, 1);
        printf("[ Song Selection ]\n");
        gotoxy(20, 18);
		printf("�� �̵� : W, S");
		gotoxy(20, 19);
		printf("���� : �����̽���");

        rewind(file);  // ���� �����͸� ó������ ������.

        int i = 0;
        while (fgets(line, sizeof(line), file)) {   // �� �پ� �о ����Ѵ�.
            int len = strlen(line);
            if (line[len - 1] == ' ') { 
                line[len - 1] = '\0'; 
            }
            
            gotoxy(10, i + 5);
            
            if (i == index) {   // ���� ���õ� �뷡 �տ� '>' ���� ǥ��
                printf("> ");
            } else {
                printf("  ");
            }
            
            printf("%s", line); 
            
            i++;
        }
		
		int prevIndex = index;	// ���� �ε��� ����
		
        int key = KeyControl();
        
		switch(key){
			case UP:
				Beep(500, 200);
				if(index > 0){
					index--;
				}
				soundPlay(index); // ���� ���		
				break;
			case DOWN:
				Beep(500, 200);
				if(index < total_lines -1) {
					index++;
				}
				soundPlay(index); // ���� ���		
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
	system("cls"); // ȭ�� �����
    int x =15, y = 6;
    gotoxy(20, 2);
    printf("[ ���� ���� ]\n");
    gotoxy(x-5, y+8);
    printf("�ٲٰ� ���� �׸��� ��ȣ�� �����ѵ� �����̽��ٸ� �����ÿ�.");
    //���� �޴� ����   
    gotoxy(x, y-1);
    printf("<�׸� ����>");
    gotoxy(x-2, y);
    printf("> 1. ���� �׸�(����: ����, ���ڻ�: ����)");
    gotoxy(x, y+1);
    printf("2. �� �׸�(����: �ϴû�, ���ڻ�: ���)");
    gotoxy(x, y+2);
    printf("3. ���� �׸�(����: ����, ���ڻ�: ���)");
    gotoxy(x, y+3);
    printf("4. �ܿ� �׸�(����: �Ͼ��, ���ڻ�: �Ķ���)");
    gotoxy(x, y+4);
    printf("5. �⺻ �׸�(����: ����, ���ڻ�: �Ͼ�)"); 
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

void ReadyView(int index){         //���� ��ŸƮ! 

	while(1){
		//3�� �ڿ� �����մϴ�. �ȳ�
        system("cls");
        gotoxy(20, 2);
        printf("[ ���� ���� ]\n");
        Sleep(1000);
        gotoxy(17, 5);
        Beep(500, 200);
        printf("3�� �ڿ� �����մϴ�.");
        Sleep(1000);
        gotoxy(17, 6);
        Beep(500, 200);
        printf("2�� �ڿ� �����մϴ�.");
        Sleep(1000);
        gotoxy(17, 7);
        Beep(500, 200);
        printf("1�� �ڿ� �����մϴ�.");
        Sleep(1000);
        gotoxy(24, 10);
        Beep(1000, 200);
        printf("����!");
        Sleep(1000);
        PlayMap(index);
	}	
}

void PlayMap(int index){
    //���� ������ִ� ���� ����
    PlaySound(NULL, NULL, SND_FILENAME | SND_ASYNC | SND_LOOP);
    //���õ� �� ���
    soundPlay(index);
    int x=24,y=4;
    while(1){
        //���� ȭ�� ���
        system("cls");
        gotoxy(20, 2);
        printf("[ ���� �÷��� ]\n\n\n");
        int score = 0;
        gotoxy(0, 4);
        int round;
        for (round = 1; round <= NUM_ROUNDS; round++) {
            printf("\nRound %d ����!\n\n", round);
            generateRandomPattern();
            showPattern();

            timer = ROUND_DURATION;
			
            while (timer > 0) {
                updateTimer();
                printf("\n����� �Է� => ");

                if (getUserInput()) {
                    if (strcmp(pattern, userInput) == 0) {
                        printf("\n����!\n");
                        score++;
                    } else {
                        printf("\n����. ������: %s\n", pattern);
                    }
                    break;
                }
            }
        
        }
        printf("���� ����! ����: %d\n", score);
        printf("�ٽ� �Ͻðڽ��ϱ�? (y/n)");
        char input = getch();
        while(1){
            if(input == 'y' || input == 'n'){
                break;
            }
        }
        if (input == 'y') {
            continue;
        } else {
            //���α׷��� ����˴ϴ� ���� �� 2�ʵ� ����
            printf("\n���α׷��� ����˴ϴ�.");
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

