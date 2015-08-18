//
//  main.c
//  DataStr_test1
//
//  Created by 민유지 on 2015. 8. 17..
//  Copyright (c) 2015년 민유지. All rights reserved.
//

//#include <stdio.h>
#include "SingleLinkedList1.h"


int main(int argc, const char * argv[]) {
// insert code here...
    
    CreateSList();
    
    int i = 1;
    while( i <= 3 )
    {
        AddNode(i);
        i++;
    }
//    AddNode(2);
//    AddNode(2);
//    AddNode(2);
    
    ShowAllNode();
    
    DelNode(2);
    
    ShowAllNode();
    
    
    
    printf("Hello, World!\n");
    return 0;
}
