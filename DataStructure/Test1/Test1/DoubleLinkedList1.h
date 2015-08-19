//
//  DoubleLinkedList1.h
//  Test1
//
//  Created by 민유지 on 2015. 8. 18..
//  Copyright (c) 2015년 민유지. All rights reserved.
//

#ifndef __Test1__DoubleLinkedList1__
#define __Test1__DoubleLinkedList1__

#include <stdio.h>
#include <stdlib.h>

#define TRUE    1
#define FALSE   0
#define HEAD    2
#define TAIL    3

typedef struct dbNode
{
    int iDbNodeData;
    struct dbNode * dbPreviousNodePtr;    // 이전 노드를 가르킬 dbNode형 포인터
    struct dbNode * dbNextNodePtr;        // 다음 노드를 가르킬 dbNode형 포인터
}DbNode;


// 초기화
void CreateDbList();

// 노드추가 - 정렬X, 무조건 앞(headOrTail에 HEAD주기)이나, 뒤(headOrTail에 TAIL)에 붙이기
int AddDbNodeNoSort( int inputData, int headOrTail );

// 노드삭제 - 정렬X, delData와 같은 노드를 모두 삭제. headOrTail : 앞이나 뒤에서 해당 delData값 찾기(아직 구현x)
int DelDbNode( int delData, int headOrTail );

// 모든 노드 보여주기
int ShowAllDbNode();

#endif /* defined(__Test1__DoubleLinkedList1__) */
