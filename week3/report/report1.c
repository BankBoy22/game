#include<stdio.h>
#include<conio.h>
#include<math.h>  


int main(void)
{
    long number = 12345698;
    printf("입력 숫자 : %ld\n\n", number);
    printf("높은 단위부터 출력\n");
    serial_number(number);
    printf("\n\n낮은 단위부터 출력\n");
    reverse_number(number);
    printf("press any key to continue...");
    getch();
    return 0;
}

void serial_number(long number)
{
    int num;
    int i, length = 0;
    length=(int)(log10(number)+1);
    for(i = length;i>=1;i--)
    {
        num = (int)(number/pow(10,i-1));
        printf("%d", num);
        number = number - num*pow(10,i-1);
    }
}

void reverse_number(long number)
{
    while(number>0)
    {
        printf("%d", number%10);
        number = number/10;
    }
}
