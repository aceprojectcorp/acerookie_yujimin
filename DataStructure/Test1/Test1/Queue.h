//
//  Queue.h
//  Test1
//
//  Created by 민유지 on 2015. 8. 19..
//  Copyright (c) 2015년 민유지. All rights reserved.
//

#ifndef __Test1__Queue__
#define __Test1__Queue__

#include <stdio.h>
#include <stdlib.h>

//노드 구조
typedef struct queueNode
{
    int iNodeData;
    struct queueNode * qPreviousPtr;    // 이전 노드를 가르킬 dbNode형 포인터
    struct queueNode * qNextPtr;        // 다음 노드를 가르킬 dbNode형 포인터
}QueueNode;

// 초기화
void CreateQueue( QueueNode ** queueHeadPtr, QueueNode ** queueTailPtr );

// 데이터 삽입
void PushQueue( QueueNode ** queueHeadPtr, QueueNode ** queueTailPtr, int inputData );

// 데이터 삭제
void PopQueue( QueueNode ** queueHeadPtr, QueueNode ** queueTailPtr );

// 모든 노드 보여주기
void ShowAllQueue( QueueNode ** queueHeadPtr, QueueNode ** queueTailPtr );


#endif /* defined(__Test1__Queue__) */
