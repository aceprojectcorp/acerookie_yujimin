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
#include "Stack.h"
#include "Queue.h"


int main(int argc, const char * argv[]) {
// insert code here...
    
//    Node * sNodeTest1;
    // 승훈님의 팁1: 요런식으로 전역변수 말고, main의 지역변수로 선언해서 사용하면 리스트 유실 없이 사용 가능.
    // 또는 Create 저 함수를 중복 사용하지 못하게 막기.
    
    printf("\n\n *** 단일연결 리스트 테스트 시작 ************\n\n");
    
    Node * sLList1 ;
    
    printf("1. CreateSList() 안하고(head생성 없이) 다른 함수 사용 -----\n");
    DelNode( &sLList1, 1);
    printf("-------------------------------------------------\n\n");
    
    // 승훈님의 팁2 : 팁1과 같이 선언하고, head의 동적할당 없얘기.. <- 스택과 큐 부터 적용해주기.

    CreateSList( &sLList1 );
    
    
    printf("\n2. 노드가 1개도 없는 상태에서 노드가 필요한 함수 사용 -----\n");
    DelNode( &sLList1, 1);
    ShowAllNode( &sLList1 );
    printf("-------------------------------------------------\n\n");
    
    // 승훈님의 팁2 : 노드 추가를 리스트의 맨앞에 추가하면 AddNode()함수가 좀 더 빠르고 효율적여짐. <- 스택과 큐 부터 적용해주기 
    printf("\n3. 노드 추가 함수 사용 후 모든 노드 출력 -----\n");
    int i = 1;
    while( i <= 5 )
    {
        AddNode( &sLList1, i);
        i++;
    }
    AddNode( &sLList1, 2);
    AddNode( &sLList1, 2);
    ShowAllNode( &sLList1 );
    printf("---------------------------------------------\n\n");
    
    
    printf("\n3. 노드 삭제 함수 사용 후 모든 노드 출력 -----\n");
    DelNode( &sLList1, 2);
    DelNode( &sLList1, 4);
    ShowAllNode( &sLList1 );
    printf("-----------------------------------------\n\n");
    
    
    printf("\n4. 리스트에 없는 데이터값을 입력한 삭제 함수 사용. -----\n");
    DelNode( &sLList1, 2);
    ShowAllNode( &sLList1 );
    printf("--------------------------------------------\n\n");
    
    
    
    
    printf("\n\n *** 이중연결 리스트 테스트 시작 ************\n\n");
    
    DbNode * dbHead1;
    DbNode * dbTail1;
    
    CreateDbList( &dbHead1, &dbTail1 );
    
    DelDbNode(&dbHead1, &dbTail1, 1, HEAD);
    ShowAllDbNode( &dbHead1, &dbTail1 );
    
    
    while ( i >= 1 ) {
        AddDbNodeNoSort( &dbHead1, &dbTail1, i, HEAD );
        i--;
    }
    
    while (i <=3 ) {
        AddDbNodeNoSort( &dbHead1, &dbTail1, i, TAIL );
        i++;
    }
    ShowAllDbNode( &dbHead1, &dbTail1 );

    
    DelDbNode(&dbHead1, &dbTail1, 1, HEAD);
    ShowAllDbNode( &dbHead1, &dbTail1 );
    
    DelDbNode(&dbHead1, &dbTail1, 2, HEAD);
    ShowAllDbNode( &dbHead1, &dbTail1 );
    
    
    
    
    printf("\n\n *** 스택 테스트 시작 ****************\n");
    
    StackNode * stackPtr ;
    
    CreateStack( &stackPtr );
    
    while( i > 1 )
    {
        Push( &stackPtr, i );
        i--;
    }
    
    ShowAllStack( &stackPtr );
    
    Pop( &stackPtr );
    
    ShowAllStack( &stackPtr );
    
    Push( &stackPtr, 5 );
    Push( &stackPtr, 2 );
    Push( &stackPtr, 77 );
    
    ShowAllStack( &stackPtr );
    
    Pop( &stackPtr );
    
    ShowAllStack( &stackPtr );
    
    
    
    
    printf("\n\n *** 큐 테스트 시작 ****************\n");
    
    QueueNode * queueHeadPtr ;
    QueueNode * queueTailPtr ;
    
    CreateQueue( &queueHeadPtr, &queueTailPtr );
    
    while( i < 6 )
    {
        PushQueue( &queueHeadPtr, &queueTailPtr, i );
        i += 2;
    }
    
    ShowAllQueue( &queueHeadPtr, &queueTailPtr );
    
    PopQueue( &queueHeadPtr, &queueTailPtr );
    
    ShowAllQueue( &queueHeadPtr, &queueTailPtr );
    
    PushQueue( &queueHeadPtr, &queueTailPtr, 77 );
    PushQueue( &queueHeadPtr, &queueTailPtr, 88 );
    
    ShowAllQueue( &queueHeadPtr, &queueTailPtr );
    
    PopQueue( &queueHeadPtr, &queueTailPtr );
    PopQueue( &queueHeadPtr, &queueTailPtr );
    PopQueue( &queueHeadPtr, &queueTailPtr );
    
    ShowAllQueue( &queueHeadPtr, &queueTailPtr );
    
    return 0;
}
