//
//  SingleLinkedList1.c
//  DataStr_test1
//
//  Created by 민유지 on 2015. 8. 17..
//  Copyright (c) 2015년 민유지. All rights reserved.
//

#include "SingleLinkedList1.h"

// 맨처음 시작 노드 가리키는(주소 저장한) 포인터
Node * nHeadPtr;

void CreateSList()
{
    nHeadPtr = (Node*)malloc(sizeof(Node));
    nHeadPtr->nodePtr = NULL;
}


// 노드추가
int AddNode( int inputData )
{
    // 노드 생성
    Node * node ;
    node = (Node*)malloc(sizeof(Node));      // 동적 할당...해야 할 것 같은데.. 음....
    node->iNodeData = inputData;
    node->nodePtr = NULL;
    
    printf("추가된 Node의 주소값 : %d\n", node);
//    printf("추가된 &Node의 주소값 : %d\n", &node); // &node가 왜 node랑 값이 다를까? ㅜㅜ <- node라는 포인터가 사용되기 위해?호출되기 위해 ?node사용시 불러오기 위해 저장된 값인감
//    printf("추가된 node->iNodeData의 주소값 : %d\n", &(node->iNodeData) );
//    printf("추가된 iNodePtr의 주소값 : %d\n", &(node->nodePtr));
//    printf("추가된 iNodePtr의 값 : %d\n", node->nodePtr);
//    

    
    // 마지막 노드 찾기. 마지막 노드의 nodePtr값이 NULL이면 마지막 노드.
    Node * searchPtr = nHeadPtr;
    
    printf("[%d] ", searchPtr->nodePtr);
    while( searchPtr->nodePtr != NULL )
    {
        searchPtr = searchPtr->nodePtr;
        printf("[%d] ", searchPtr->nodePtr);
    }
    
    printf("\n");
    
    //nodePtr값이 0인 노드가 발견되면 (마지막노드가 발견!!!!)
    searchPtr->nodePtr = node;
    printf("[%d]\n ", searchPtr->nodePtr);
    
    printf("%d 추가\n\n", inputData );
    
    return TRUE;
}

int DelNode( int delData )
{
    
    Node * selectNode = nHeadPtr;   // 검색시 선택된 노드 (delData와 노드의 iNodeData값을 비교함)
    Node * tmpNode = NULL;                 // 검색시 선택된 노드를 임시 저장할 변수
    Node * frontNode = NULL;        // 검색시 선택된 노드의 이전 노드를 임시 저장할 변수
                                    // 이중포인터...쓰면 안써도되지만
    int delNodeCount = 0;           // 삭제된 노드 갯수 카운트

    
    // 처음부터 끝까지 검색
    while( selectNode != NULL )
    {
        if( selectNode->iNodeData == delData )
        {
            tmpNode = selectNode;
            
            frontNode->nodePtr = selectNode->nodePtr;
            
            selectNode = selectNode->nodePtr;
            
            free(tmpNode);                      // 이것도 안됨
            
            delNodeCount++;
        }
        else
        {
            frontNode = selectNode;
            selectNode = selectNode->nodePtr;
        }
    }
    
    printf("\n* 삭제된 노드의 갯수 : %d \n",delNodeCount);
    
    return TRUE;
    
}

int ShowAllNode()
{
    int iNodeCnt = 0 ; // 노드 갯수 카운트
    printf("\n\n* 모든 노드 출력\n");
    Node * searchPtr = nHeadPtr;
    while( searchPtr->nodePtr != NULL )
    {
        searchPtr = searchPtr->nodePtr;
        iNodeCnt++;
        printf("<%d> ", searchPtr->iNodeData);
    }
    printf("\n - 전체 노드 갯수 : %d\n",iNodeCnt );
    printf("\n");
    return TRUE;
}


// 노드 찾는 재귀함수 삭제
// 변수값 수정 iHeadPtr -> nHeadPtr
