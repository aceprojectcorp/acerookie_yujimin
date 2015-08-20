//
//  Queue.c
//  Test1
//
//  Created by 민유지 on 2015. 8. 19..
//  Copyright (c) 2015년 민유지. All rights reserved.
//

#include "Queue.h"


void CreateQueue( QueueNode ** queueHeadPtr, QueueNode ** queueTailPtr )
{
    printf("\n* 큐 생성(초기화)\n\n");
    *queueHeadPtr = NULL;
    *queueTailPtr = NULL;
}


void PushQueue( QueueNode ** queueHeadPtr, QueueNode ** queueTailPtr, int inputData )
{
    printf("* 큐에 데이터 삽입 : %d인 데이터 삽입 \n\n", inputData );
    
    // 노드 생성
    QueueNode * node ;
    node = (QueueNode*)malloc(sizeof(QueueNode));
    node->iNodeData = inputData;
    node->qNextPtr = NULL;
    node->qPreviousPtr = NULL;
    
    // 노드를 리스트에 삽입
    // 노드가 1개도 없을 경우. head와 tail의 포인터가 null을 카르키는 상태.
    if ( *queueTailPtr == NULL )
    {
        *queueTailPtr = *queueHeadPtr = node;
    }
    else
    {
        (*queueTailPtr)->qPreviousPtr = node;
        
        node->qNextPtr = *queueTailPtr;
        
        *queueTailPtr = node;
    }
    
//    printf(" 추가된 Node의 주소값 : %p\n\n", node);
}


void PopQueue( QueueNode ** queueHeadPtr, QueueNode ** queueTailPtr )
{
    printf("* 큐의 데이터 삭제 : 제일 마지막에 삽입된 데이터 삭제 \n" );
    
    // 스택에 데이터가 한개도 없으면
    if ( *queueTailPtr == NULL )
    {
        printf(" ~ 삭제할 데이터가 없습니다\n\n");
        return;
    }
    else
    {
        printf(" ~ 삭제될 데이터 : %d\n", (*queueHeadPtr)->iNodeData );
    
        QueueNode * delNode = *queueHeadPtr ;
        
        // 딱 1개있던 데이터를 삭제하는 경우.
        if( *queueHeadPtr == *queueTailPtr )
        {
            *queueTailPtr = *queueHeadPtr = NULL ;
        }
        else
        {
            *queueHeadPtr = (*queueHeadPtr)->qPreviousPtr ;
            (*queueHeadPtr)->qNextPtr = NULL;
        }
        free( delNode );
    }
    printf("\n");
}


void ShowAllQueue( QueueNode ** queueHeadPtr, QueueNode ** queueTailPtr )
{
    printf("* 큐 출력\n");
    
    // 보여줄 노드가 있는지 확인
    if ( *queueHeadPtr == NULL )
    {
        printf(" ~ 출력할 데이터가 없습니다\n\n");
        return;
    }
    else
    {
        QueueNode * search = *queueHeadPtr ;
        while ( search != NULL ) {
            printf("%d\n", search->iNodeData );
            search = search->qNextPtr;
        }
    }
    
    printf("\n");
}