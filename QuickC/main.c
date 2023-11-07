#include <stdio.h>

void GetName();

void IntCollect();

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
    IntCollect();
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


struct Student {
    int id;
};