#include <stdio.h>
#include "math.h"

void GetName();

void IntCollect();

void ShuiXianHua();

int main() {

//    const int AMOUNT = 100;
//    printf("Ok, One Week\n");
//    int a = 0;
//    int b = 0;
//    scanf("%d",&a);
//    scanf("%d", &b);
//    printf("%d",AMOUNT + a + b);
//    printf(" a++%d",a++);
//    printf(" a%d",a);
//    GetName();
//    IntCollect();
    ShuiXianHua();
    return 0;
}

void GetName() {
    printf("ssss");
}

void IntCollect() {
    int a = 0;
    scanf("%d", &a);
    int i, j, k;
    for (int s = a; s <= a + 3; ++s) {
        i = s;
        int count = 0;
        for (int l = a; l <= a + 3; ++l) {
            j = l;
            for (int m = a; m <= a + 3; ++m) {
                k = m;
                if (i != j && j != k && i != k) {
                    printf("%d%d%d", i,j,k);
                    count ++;
                    if(count == 6)
                        printf("\n");
                    else
                        printf(" ");
                }
            }
        }
    }
}

void ShuiXianHua()
{
    int n = 0;
    scanf("%d", &n);
    int max = 100;
    int start = 0;
    for (int i = 1; i < n - 1; ++i) {
        max *= 10;
    }
    start = max/10;
    for (int i = start; i < max; ++i) {
        int num = 0;
        int sum = 0;
        int temp = i;
        while (num < n)
        {
            sum += (int)pow(temp%10,n);
            temp /= 10;
            num++;
        }
        if(sum == i)
        {
            printf("%d\n", i);
        }
    }
}


struct Student {
    int id;
};