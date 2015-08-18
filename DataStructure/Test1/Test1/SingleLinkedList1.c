//
//  SingleLinkedList1.c
//  DataStr_test1
//
//  Created by 민유지 on 2015. 8. 17..
//  Copyright (c) 2015년 민유지. All rights reserved.
//

#include "SingleLinkedList1.h"

// 맨처음 시작 노드 가리키는(주소 저장한) 포인터
Node * nHeadPtr = NULL;

void CreateSList()
{
    printf("* 리스트 생성(head생성)\n\n");
    nHeadPtr = (Node*)malloc(sizeof(Node));
}


// 노드추가
int AddNode( int inputData )
{
    
    printf("* 노드 생성 : 데이터가 %d인 노드 \n", inputData );
    
    // 노드 생성
    Node * node ;
    node = (Node*)malloc(sizeof(Node));
    node->iNodeData = inputData;
    node->nodePtr = NULL;
    
    printf(" 추가된 Node의 주소값 : %p\n", node);

    
    // 마지막 노드 찾기. 마지막 노드의 nodePtr값이 NULL이면 마지막 노드.
    Node * searchPtr = nHeadPtr;
    
    printf("[%d / %p]((H)) ", searchPtr->iNodeData, searchPtr->nodePtr);
    while( searchPtr->nodePtr != NULL )
    {
        searchPtr = searchPtr->nodePtr;
        printf("[%d / %p] ", searchPtr->iNodeData, searchPtr->nodePtr);
    }
    
    printf("\n");
    
    //nodePtr값이 0인 노드가 발견되면 (마지막노드가 발견!!!!)
    searchPtr->nodePtr = node;
    printf("[%d / %p] ", searchPtr->iNodeData, searchPtr->nodePtr);
    printf("[%d / %p]\n", node->iNodeData, node->nodePtr);
    
    printf(" ~ %d 추가완료\n\n", inputData );
    
    return TRUE;
}

int DelNode( int delData )
{
    
    printf("* 노드 삭제 : 데이터가 %d인 노드 모두 삭제 \n", delData );
    
    // 삭제할 노드가 있는지 확인
    if ( nHeadPtr == NULL || nHeadPtr->nodePtr == NULL )
    {
        printf(" ~ 삭제할 노드가 없습니다\n\n");
        
        return TRUE;
    }
    
    Node * selectNode = nHeadPtr;   // 선택된 노드. 노드 주소를 이동 할때만 사용.
    Node * frontNode = NULL;        // 선택된 노드의 이전 노드를 저장할 변수.
                                    // ex) [이전노드]-[삭제노드]-[삭제다음노드] : 이전노드에서 삭제다음노드로 이어주기 위해 이전노드를 저장.
    int iDelNodeCount = 0;           // 삭제된 노드 갯수 카운트

    
    // 처음부터 끝까지 검색
    while( selectNode != NULL )
    {
        if( selectNode->iNodeData == delData )
        {
            frontNode->nodePtr = selectNode->nodePtr;
  
            free(selectNode);
            
            selectNode = selectNode->nodePtr;
            iDelNodeCount++;
        }
        else
        {
            frontNode = selectNode;
            selectNode = selectNode->nodePtr;
        }
    }
    printf(" ~ 삭제된 노드의 갯수 : %d \n\n",iDelNodeCount);
    
    return TRUE;
    
}

int ShowAllNode()
{
    
    printf("* 모든 노드 리스트 출력\n");
    
    // 보여줄 노드가 있는지 확인
    if ( nHeadPtr==NULL || nHeadPtr->nodePtr == NULL ) {
        printf(" ~ 출력할 노드가 없습니다\n\n");
        
        return TRUE;
    }
    
    int iNodeCnt = 0 ; // 노드 갯수 카운트
    Node * searchPtr = nHeadPtr;
    while( searchPtr->nodePtr != NULL )
    {
        searchPtr = searchPtr->nodePtr;
        iNodeCnt++;
        printf("<%d> ", searchPtr->iNodeData);
    }
    printf("\n ~ 전체 노드 갯수 : %d\n\n",iNodeCnt );
    return TRUE;
}


// 노드 찾는 재귀함수 삭제
// 변수값 수정 iHeadPtr -> nHeadPtr
