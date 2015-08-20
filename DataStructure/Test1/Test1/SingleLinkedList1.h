//
//  SingleLinkedList1.h
//  DataStr_test1
//
//  Created by 민유지 on 2015. 8. 17..
//  Copyright (c) 2015년 민유지. All rights reserved.
//

#ifndef __DataStr_test1__SingleLinkedList1__
#define __DataStr_test1__SingleLinkedList1__

#include <stdio.h>
#include <stdlib.h>


#define TRUE    1
#define FALSE   0

//노드 구조
typedef struct node
{
    // 저장할 데이터
    int iNodeData;
    
    // 다음 노드의 위치를 저장하는 포인터
    struct node * nodePtr;
    
}Node;


// 초기화
void CreateSList( Node ** ptr );

// 노드추가
int AddNode( Node ** ptr, int inputData );

// 노드삭제
int DelNode( Node ** ptr, int delData );

// 모든 노드 보여주기
int ShowAllNode( Node ** ptr );


#endif /* defined(__DataStr_test1__SingleLinkedList1__) */
