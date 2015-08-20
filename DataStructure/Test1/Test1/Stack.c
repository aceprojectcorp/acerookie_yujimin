//
//  Stack.c
//  Test1
//
//  Created by 민유지 on 2015. 8. 19..
//  Copyright (c) 2015년 민유지. All rights reserved.
//

#include "Stack.h"


void CreateStack( StackNode ** stackPtr )
{
    printf("\n* 스택 생성(초기화)\n\n");
    *stackPtr = NULL;
}


void Push( StackNode ** stackPtr, int inputData )
{    
    printf("* 데이터 삽입 : %d인 데이터 삽입 \n", inputData );
    
    // 노드 생성
    StackNode * node ;
    node = (StackNode*)malloc(sizeof(StackNode));
    node->iNodeData = inputData;
    node->nodePtr = NULL;
    
    printf(" ~ 추가된 데이터의 주소값 : %p\n", node);
    
    // stack이 안 비어있을 경우(데이터가 1개이상 있는 상황)
    if( *stackPtr != NULL )
    {
        node->nodePtr = *stackPtr;
    }
    *stackPtr = node;
    
    printf(" ~ %p 다음 주소값\n", node->nodePtr );
    printf(" ~ %d 추가완료\n\n", inputData );
}


void Pop( StackNode ** stackPtr )
{
    printf("* 데이터 삭제 : 제일 최근에 삽입된 데이터 삭제 \n" );
    
    // 스택에 데이터가 한개도 없으면
    if ( *stackPtr == NULL )
    {
        printf(" ~ 삭제할 데이터가 없습니다\n\n");
        return;
    }
    else
    {
        printf(" ~ 삭제될 데이터 : %d\n", (*stackPtr)->iNodeData );
        
        free( *stackPtr );
        
        // 딱 1개있던 데이터를 삭제하는 경우.
        if( (*stackPtr)->nodePtr == NULL )
        {
            *stackPtr = NULL ;
        }
        else
        {
            *stackPtr = ((*stackPtr)->nodePtr) ;
        }
    }
    
    printf("\n");
}


void ShowAllStack( StackNode ** stackPtr )
{
    printf("* 스택안에 모든 데이터 출력\n");
    
    if ( *stackPtr == NULL ) {
        printf(" ~ 스택안에 데이터가 없습니다.\n");
    }
    else
    {
        StackNode * search = *stackPtr;
        while ( search != NULL ) {
            printf("%d\n", search->iNodeData );
            search = search->nodePtr;
        }
    }
    
    printf("\n");
}