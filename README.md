# hello-world
#include <stdlib.h>
#include <conio.h>
#include <time.h>
#define _USE_MATH_DEFINES
#include <math.h>
using namespace std;

double func_laplace(double a, double precision) {
    double result = 0;
    for (double i = 0; i < a; i += precision) {
        result += precision * fabs(pow(M_E, -.5 * pow(i, 2)) + pow(M_E, -.5 * pow((i + precision), 2))) / 2.0;
    }
    result *= 1.0 / pow(2 * M_PI, .5);
    return result;
}
int main() {
    setlocale(0, "");
    double a, precision;
    do {
        cout << "a = "; cin >> a;
        //cout << "precision = "; cin >> precision;
        cout << func_laplace(a, .0001) << endl;
        cout << "More?(y)..." << endl;
    } while (_getch() == 121);

    return 0;
}

