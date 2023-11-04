#include <stdio.h>

void GetName();

int main() {

    const int AMOUNT = 100;
    printf("Ok, One Week\n");
    int a = 0;
    int b = 0;
    scanf("%d",&a);
    scanf("%d", &b);
    printf("%d",AMOUNT + a + b);
    printf(" a++%d",a++);
    printf(" a%d",a);
    GetName();
    return 0;
}

void GetName()
{
    printf("ssss");
}

struct Student
{
    int id;
};