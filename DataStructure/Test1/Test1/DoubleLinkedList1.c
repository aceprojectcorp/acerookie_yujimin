//
//  DoubleLinkedList1.c
//  Test1
//
//  Created by 민유지 on 2015. 8. 18..
//  Copyright (c) 2015년 민유지. All rights reserved.
//

#include "DoubleLinkedList1.h"

// 맨처음 시작 노드 가리키는(주소 저장한) 포인터
DbNode * dbHeadPtr = NULL;
DbNode * dbTailPtr = NULL;

void CreateDbList()
{
    printf("* 이중연결리스트 생성(head생성)\n\n");
    dbHeadPtr = (DbNode*)malloc(sizeof(DbNode));
    
    dbHeadPtr->dbNextNodePtr = NULL;
    
    dbTailPtr = (DbNode*)malloc(sizeof(DbNode));
    dbTailPtr->dbPreviousNodePtr = NULL;

    
}

int ShowAllDbNode()
{
    printf("* DoubleLinkedList의 모든 노드 리스트 출력\n");
    
    // 보여줄 노드가 있는지 확인
    if ( dbHeadPtr->dbNextNodePtr == NULL )
    {
        printf(" ~ 출력할 노드가 없습니다\n\n");
        
        return TRUE;
    }
    
    DbNode * searchPtr = dbHeadPtr ;   // 검색용 노드형 변수
    int iNodeCnt = 0 ;                              // 전체 노드 갯수 카운트
    
    //여기서 1번째 노드를 안세네 !!!!! 흫ㅎㅊㅎ히힣ㅎ찾았다!!!!
    // 아래 while문 때문에 첫번째 노드를 카운트 안함!!!!
    while ( searchPtr->dbNextNodePtr != NULL ) // && dbTailPtr->dbPreviousNodePtr != NULL )  // <- 만약 중간에 절단될 경우
    {
        searchPtr = searchPtr->dbNextNodePtr;
        printf("<%d> ", searchPtr->iDbNodeData);
        iNodeCnt++;
    }
    printf("\n ~ 전체 노드 갯수 : %d\n\n",iNodeCnt );
    
    
    
    return TRUE;
}

int AddDbNodeNoSort( int inputData, int headOrTail )
{
    printf("* 이중 노드 생성(정렬X) : 데이터가 %d인 노드 \n", inputData );
    
    // 노드 생성
    DbNode * node ;
    node = (DbNode*)malloc(sizeof(DbNode));      // 동적 할당...해야 할 것 같은데.. 음....
    node->iDbNodeData = inputData;
    node->dbPreviousNodePtr = NULL;
    node->dbNextNodePtr = NULL;
    
    DbNode * tmpNode ;
    
    // 노드를 리스트에 삽입
    // 노드가 1개도 없을 경우. head와 tail의 포인터가 null을 카르킴.
    if ( dbHeadPtr->dbNextNodePtr == NULL )
    {
        dbHeadPtr->dbNextNodePtr = node;
        dbTailPtr->dbPreviousNodePtr = node;
    }
    else if ( headOrTail == HEAD )
    {
        tmpNode = dbHeadPtr->dbNextNodePtr;
        
        node->dbNextNodePtr = dbHeadPtr->dbNextNodePtr ;
        
        tmpNode->dbPreviousNodePtr = node;
        
        dbHeadPtr->dbNextNodePtr = node;
        
    }
    //headOrTail == TAIL
    else
    {
        // 상황  ...[노드]<->[마지막노드]<- tail(pre)       [새 노드]
        tmpNode = dbTailPtr->dbPreviousNodePtr ;                    // tail이 가르키던 마지막 노드를 가르키게 됨.
        
        node->dbPreviousNodePtr = dbTailPtr->dbPreviousNodePtr ;    // 새노드 pre값에 tail이 가르키던 마지막 노드 연결
        
        tmpNode->dbNextNodePtr = node;                              // 마지막 노드의 next값에 새 노드 연결

        dbTailPtr->dbPreviousNodePtr = node ;                       // tail의 pre값에 마지막 노드 연결
        
    }
    
    printf(" 추가된 Node의 주소값 : %p\n\n", node);
    
    return TRUE;
}

int DelDbNode( int delData, int headOrTail )
{
    printf("* 이중 노드 삭제 : 데이터가 %d인 노드 모두 삭제 \n", delData );
    
    // 삭제할 노드가 있는지 확인
    if ( dbHeadPtr == NULL || dbHeadPtr->dbNextNodePtr == NULL )
    {
        printf(" ~ 삭제할 노드가 없습니다\n\n");
        
        return TRUE;
    }
    
    return TRUE ;
}