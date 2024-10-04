#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <cstring>

short EAN13[13];

static bool left_A[10][7] = {
        {0,0,0,1,1,0,1},
        {0,0,1,1,0,0,1},
        {0,0,1,0,0,1,1},
        {0,1,1,1,1,0,1},
        {0,1,0,0,0,1,1},
        {0,1,1,0,0,0,1},
        {0,1,0,1,1,1,1},
        {0,1,1,1,0,1,1},
        {0,1,1,0,1,1,1},
        {0,0,0,1,0,1,1}
};

bool left_B[10][7] = {};

bool right_C[10][7] = {};

static bool A0_or_B1[10][6] = {
        /*0*/ {0,0,0,0,0,0},
        /*1*/ {0,0,1,0,1,1},
        /*2*/ {0,0,1,1,0,1},
        /*3*/ {0,0,1,1,1,0},
        /*4*/ {0,1,0,0,1,1},
        /*5*/ {0,1,1,0,0,1},
        /*6*/ {0,1,1,1,0,0},
        /*7*/ {0,1,0,1,0,1},
        /*8*/ {0,1,0,1,1,0},
        /*9*/ {0,1,1,0,1,0}
};

void controlNumber() {

    // sum even numbers + 3 * odd numbers
    int sum =
            1 * EAN13[0] +
            3 * EAN13[1] +
            1 * EAN13[2] +
            3 * EAN13[3] +
            1 * EAN13[4] +
            3 * EAN13[5] +
            1 * EAN13[6] +
            3 * EAN13[7] +
            1 * EAN13[8] +
            3 * EAN13[9] +
            1 * EAN13[10] +
            3 * EAN13[11];

    sum %= 10;
    sum = 10 - sum;  // complement to 10
    if (sum == 10)sum = 0;

    EAN13[12] = sum;  // control sum at last index
}


int main()
{
    int first;
    char temporary[200];
    bool code[95];

    // variables initialization
    for (int i = 0; i < 10; i++) {
        for (int j = 0; j < 7; j++)
        {
            right_C[i][j] = !(left_A[i][j]); // C code -> negation A code
            left_B[i][6 - j] = right_C[i][j]; // B code -> reversed C code
        }
    }

    // Numbers insert
    printf("Insert 12 numbers of EAN code\n");
    std::cin >> temporary;

    // Text to numbers convertion
    for (int i = 0; i < 12; i++) {
        EAN13[i] = temporary[i] - '0';
    }

    first = EAN13[0];

    // Control sum
    controlNumber();

    // 1D code count
    code[0] = 1; // start / stop code
    code[1] = 0;
    code[2] = 1;


    // First 7 numbers convertion into A or B code (left hand side)
    for (int i = 0; i < 6; i++) {
        // A = 0
        // B = 1

        // First number decides if we code using A or B code
        if (A0_or_B1[first][i]) {
            // B version
            // 7 wide
            for (int j = 0; j < 7; j++) {
                code[3 + i * 7 + j] = left_B[EAN13[i + 1]][j];
            }

        }
        else {
            // A version
            // 7 wide
            for (int j = 0; j < 7; j++) {
                code[3 + i * 7 + j] = left_A[EAN13[i + 1]][j];
            }
        }
    }

    code[45] = 0; // middle break code
    code[46] = 1;
    code[47] = 0;
    code[48] = 1;
    code[49] = 0;

    // Change last 6 numberst into C code (right hand side)
    for (int i = 6; i < 12; i++) {
        for (int j = 0; j < 7; j++) {
            code[8 + i * 7 + j] = right_C[EAN13[i + 1]][j];
        }
    }

    code[92] = 1; // Start / stop code
    code[93] = 0;
    code[94] = 1;


    // Print EAN code in console
    for (int xx = 0; xx < 30; xx++) {

        for (int i = 0; i < 11; i++) {
            // left margin
            printf("%c", 219);
        }

        // black-white EAN code
        for (int i = 0; i < 95; i++) {
            if (code[i]) {
                // black
                printf(" ");
            }
            else {
                // white
                printf("%c", 219);
            }
        }

        for (int i = 0; i < 11; i++) {
            // right margin
            printf("%c", 219);
        }

        printf("\n");
    }


    FILE *file;
    unsigned char *image = NULL;
    int file_size = 54 + 3 * 1295*360;
    image = (unsigned char *)malloc(3 * 1295 * 360);
    memset(image, 0, 3 * 1295 * 360);

    // Width
    for (int i = 0; i < 1295; i++)
    {
        // Height
        for (int j = 0; j < 360; j++)
        {
            int y = (360 - 1) - j;

            if (i > 30 && i < 1265 && j > 30 && (j < 330)) {
                // Translate code into white or black vertical line
                if (code[(i - 30) / 13]) {
                    // Black
                    image[(i + y * 1295) * 3 + 2] = (unsigned char)(0);
                    image[(i + y * 1295) * 3 + 1] = (unsigned char)(0);
                    image[(i + y * 1295) * 3 + 0] = (unsigned char)(0);
                }
                else {
                    // White
                    image[(i + y * 1295) * 3 + 2] = (unsigned char)(255);
                    image[(i + y * 1295) * 3 + 1] = (unsigned char)(255);
                    image[(i + y * 1295) * 3 + 0] = (unsigned char)(255);
                }
            }
                // Cover margin
            else {
                // White margin
                image[(i + y * 1295) * 3 + 2] = (unsigned char)(255);
                image[(i + y * 1295) * 3 + 1] = (unsigned char)(255);
                image[(i + y * 1295) * 3 + 0] = (unsigned char)(255);
            }
        }
    }

    unsigned char bmp_file_header[14] = { 'B','M', 0,0,0,0, 0,0, 0,0, 54,0,0,0 };
    unsigned char bmp_info_header[40] = { 40,0,0,0, 0,0,0,0, 0,0,0,0, 1,0, 24,0 };
    unsigned char bmppad[3] = { 0,0,0 };

    bmp_file_header[2] = (unsigned char)(file_size);
    bmp_file_header[3] = (unsigned char)(file_size >> 8);
    bmp_file_header[4] = (unsigned char)(file_size >> 16);
    bmp_file_header[5] = (unsigned char)(file_size >> 24);

    bmp_info_header[4] = (unsigned char)(1295);
    bmp_info_header[5] = (unsigned char)(1295 >> 8);
    bmp_info_header[6] = (unsigned char)(1295 >> 16);
    bmp_info_header[7] = (unsigned char)(1295 >> 24);
    bmp_info_header[8] = (unsigned char)(360);
    bmp_info_header[9] = (unsigned char)(360 >> 8);
    bmp_info_header[10] = (unsigned char)(360 >> 16);
    bmp_info_header[11] = (unsigned char)(360 >> 24);

    // Begining of saving image into file
    fopen_s(&file,"code.bmp", "wb");
    fwrite(bmp_file_header, 1, 14, file);
    fwrite(bmp_info_header, 1, 40, file);

    for (int i = 0; i < 360; i++)
    {
        fwrite(image + (1295*(360 - i - 1) * 3), 3, 1295, file);
        fwrite(bmppad, 1, (4 - (1295 * 3) % 4) % 4, file);
    }

    // End of save
    free(image);
    fclose(file);

    return 0;
}