#include <iostream>
using namespace std;

// // #1
// int main() {
//     for (int i = 1; i <= 3; i++) 
//     {
//         for (int j = 1; j <= 2; j++) {
//             cout << j << i << "\n";
//         }
//     }
// }

// // #2
// int main() {
//     for (int i = 1; i <= 2; i++) 
//     {

    
//         for (int j = 1; j <= 5; j++) {
//             cout << j << i;
//         }
//     cout << "\n";
//     }
// }

// // #3
// int main() {
//     for (int i = 1; i <= 3; i++) 
//     {

    
//         for (int j = 1; j > 5; j++) {
//             cout << j << i;
//         }
//     cout << "\n";
//     }
// }

// // #4
// int main() {
//     for (int i = 1; i <= 3; i++) 
//     {

    
//         for (int j = 1; j <= 5; j++) {
//             cout << "*";
//         }
//     cout << "\n";
//     }
// }

// // #5 pattern
// int main() {
//     for (int i = 1; i <= 4; i++) 
//     {

    
//         for (int j = 1; j <= i; j++) {
//             cout << "*";
//         }
//     cout << "\n";
//     }
// }

// // #6 reverse pattern
// int main() {
//     for (int i = 1; i <= 4; i++) 
//     {

    
//         for (int j = 4; j >= i; j--) {
//             cout << "*";
//         }
//     cout << "\n";
//     }
// }

// // #7 sideways traingle
// int main() {
//     for (int i = 1; i <= 4; i++) 
//     {

    
//         for (int j = 1; j <= i; j++) {
//          cout << "*";
//         }
//     cout << "\n";
//     }
    
//     for (int i = 1; i <= 3; i++) 
//     {

    
//         for (int j = 3; j >= i; j--) {
//          cout << "*";
//         }
//     cout << "\n";
//     }
// }

// // #8 reverse coloum
// int main() 
// {

//     // int pattern;
//     // cout << "Enter the pattern row you want for pattern: ";
//     // cin >> pattern;
//     for (int i = 1; i <= 4; i++) 
//     {
//         for (int j = 1; j <= 4 - i; j++) 
//         {
//          cout << " ";
//         }

//         for (int k = 1; k <= i; k++) 
//         {
//          cout << "*";
//         }
//         cout << "\n";
//     }   
// }

// // #9 user input pattern 
// int main() {
//     int pattern;
//     cout << "Enter the pattern row you want for pattern: ";
//     cin >> pattern;
//     for (int i = 1; i <= pattern; i++) 
//     {

    
//         for (int j = 1; j <= i; j++) {
//             cout << "*";
//         }
//     cout << "\n";
//     }
// }

// // #10 user input pattern 
// int main() {
//     int pattern;
//     cout << "Enter the pattern row you want for pattern: ";
//     cin >> pattern;
//     for (int i = 1; i <= pattern; i++) 
//     {

    
//         for (int j = pattern; j >= i; j--) {
//             cout << "*";
//         }
//     cout << "\n";
//     }
// }

// // #11 user input sideways triangle
// int main() {
//     int pattern;
//     cout << "Enter the pattern row you want for pattern: ";
//     cin >> pattern;
//     for (int i = 1; i <= pattern; i++) 
//     {

    
//         for (int j = 1; j <= i; j++) {
//          cout << "*";
//         }
//     cout << "\n";
//     }
    
//     for (int i = 1; i < pattern; i++) 
//     {

    
//         for (int j = pattern; j > i; j--) {
//          cout << "*";
//         }
//     cout << "\n";
//     }
// }

// // #12 user input reverse coloum
// int main() 
// {

//     int pattern;
//     cout << "Enter the pattern row you want for pattern: ";
//     cin >> pattern;
//     for (int i = 1; i <= pattern; i++) 
//     {
//         for (int j = 1; j <= pattern - i; j++) 
//         {
//          cout << " ";
//         }

//         for (int k = 1; k <= i; k++) 
//         {
//          cout << "*";
//         }
//         cout << "\n";
//     }   
// }

// // #13 reverse coloum + row
// int main() 
// {

//     // int pattern;
//     // cout << "Enter the pattern row you want for pattern: "; 
//     // cin >> pattern;
//     for (int i = 1; i <= 4; i++) 
//     {
//         for (int j = 1; j <= i - 1; j++) 
//         {
//          cout << " ";
//         }

//         for (int k = 4; k >= i; k--) 
//         {
//          cout << "*";
//         }
//         cout << "\n";
//     }   
// }

// // #14 user input reverse coloum + row
// int main() 
// {

//     int pattern;
//     cout << "Enter the pattern row you want for pattern: "; 
//     cin >> pattern;
//     for (int i = 1; i <= pattern; i++) 
//     {
//         for (int j = 1; j <= i - 1; j++) 
//         {
//          cout << " ";
//         }

//         for (int k = pattern; k >= i; k--) 
//         {
//          cout << "*";
//         }
//         cout << "\n";
//     }   
// }

// // #15 user input number pattern
// int main() {
//     int pattern;
//     cout << "Enter the pattern row you want for pattern: ";
//     cin >> pattern;
//     for (int i = 1; i <= pattern; i++) 
//     {
//         for (int j = 1; j <= i; j++) 
//         {
//             cout << j;
//         }
//     cout << "\n";
//     }
// }

// #15 user input number pattern
int main() {
    int pattern;

    int x = 1;
    cout << "Enter the pattern row you want for pattern: ";
    cin >> pattern;
    for (int i = 1; i <= pattern; i++) 
    {
        for (int j = 1; j <= i; j++) 
        {
            cout << x;
            x++;
        }
    cout << "\n";
    }
}

