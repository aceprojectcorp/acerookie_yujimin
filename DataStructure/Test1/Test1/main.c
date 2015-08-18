//
//  main.c
//  DataStr_test1
//
//  Created by 민유지 on 2015. 8. 17..
//  Copyright (c) 2015년 민유지. All rights reserved.
//

//#include <stdio.h>
#include "SingleLinkedList1.h"
#include "DoubleLinkedList1.h"


int main(int argc, const char * argv[]) {
// insert code here...
    
    printf("1. CreateSList() 안하고(head생성 없이) 다른 함수 사용 -----\n");
    DelNode(1);
    printf("-------------------------------------------------\n\n");
    
    
    CreateSList();
    
    
    printf("\n2. 노드가 1개도 없는 상태에서 노드가 필요한 함수 사용 -----\n");
    DelNode(1);
    ShowAllNode();
    printf("-------------------------------------------------\n\n");
    

    printf("\n3. 노드 추가 함수 사용 후 모든 노드 출력 -----\n");
    int i = 1;
    while( i <= 5 )
    {
        AddNode(i);
        i++;
    }
    AddNode(2);
    AddNode(2);
    ShowAllNode();
    printf("---------------------------------------------\n\n");
    
    
    printf("\n3. 노드 삭제 함수 사용 후 모든 노드 출력 -----\n");
    DelNode(2);
    DelNode(4);
    ShowAllNode();
    printf("-----------------------------------------\n\n");
    
    
    printf("\n4. 리스트에 없는 데이터값을 입력한 삭제 함수 사용. -----\n");
    DelNode(2);
    ShowAllNode();
    printf("--------------------------------------------\n\n");
    
    CreateDbList();
    
    AddDbNodeNoSort( 1, HEAD );
    ShowAllDbNode();
    
    AddDbNodeNoSort( 2, HEAD );
    ShowAllDbNode();
    
    AddDbNodeNoSort( 3, TAIL );
    ShowAllDbNode();
    
    return 0;
}
