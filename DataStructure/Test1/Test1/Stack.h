//
//  Stack.h
//  Test1
//
//  Created by 민유지 on 2015. 8. 19..
//  Copyright (c) 2015년 민유지. All rights reserved.
//

#ifndef __Test1__Stack__
#define __Test1__Stack__

#include <stdio.h>
#include <stdlib.h>

//노드 구조
typedef struct stackNode
{
    int iNodeData;
    struct stackNode * nodePtr;
}StackNode;

// 초기화
void CreateStack( StackNode ** stackPtr );

// 데이터 삽입
void Push( StackNode ** stackPtr, int inputData );


// 데이터 삭제
void Pop( StackNode ** stackPtr );

// 모든 노드 보여주기
void ShowAllStack( StackNode ** stackPtr );


#endif /* defined(__Test1__Stack__) */
