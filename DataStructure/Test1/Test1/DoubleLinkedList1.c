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
    printf("* 이중연결리스트 생성(head, tail생성)\n\n");
    dbHeadPtr = (DbNode*)malloc(sizeof(DbNode));
    
    dbHeadPtr->dbNextNodePtr = NULL;
    
    dbTailPtr = (DbNode*)malloc(sizeof(DbNode));
    dbTailPtr->dbPreviousNodePtr = NULL;
}

int ShowAllDbNode()
{
    printf("* DoubleLinkedList의 모든 노드 리스트 출력\n");
    printf(" ~ Head : %p\n", dbHeadPtr->dbNextNodePtr );
    printf(" ~ Tail : %p\n", dbTailPtr->dbPreviousNodePtr );

    // 보여줄 노드가 있는지 확인
    if ( dbHeadPtr->dbNextNodePtr == NULL )
    {
        printf(" ~ 출력할 노드가 없습니다\n\n");
        
        return TRUE;
    }
    
    DbNode * searchPtr = dbHeadPtr ;   // 검색용 노드형 변수
    int iNodeCnt = 0 ;                 // 전체 노드 갯수 카운트
    
    while ( searchPtr->dbNextNodePtr != NULL ) // && dbTailPtr->dbPreviousNodePtr != NULL )  // <- 만약 중간에 절단될 경우
    {
        searchPtr = searchPtr->dbNextNodePtr;
        printf("< %d / %p\t/ (%p)\t / %p >\n", searchPtr->iDbNodeData, searchPtr->dbPreviousNodePtr, searchPtr, searchPtr->dbNextNodePtr );
        iNodeCnt++;
    }
    printf(" ~ 전체 노드 갯수 : %d\n\n",iNodeCnt );
    
    
    
    return TRUE;
}

int AddDbNodeNoSort( int inputData, int headOrTail )
{
    printf("* 이중 노드 생성(정렬X) : 데이터가 %d인 노드 ", inputData );
    if( headOrTail == HEAD )
        printf("(앞으로입력)\n");
    else
        printf("(뒤로입력)\n");
    
    // 노드 생성
    DbNode * node ;
    node = (DbNode*)malloc(sizeof(DbNode));
    node->iDbNodeData = inputData;
    node->dbPreviousNodePtr = NULL;
    node->dbNextNodePtr = NULL;
    
    
    // 노드를 리스트에 삽입
    // 노드가 1개도 없을 경우. head와 tail의 포인터가 null을 카르킴.
    if ( dbHeadPtr->dbNextNodePtr == NULL )
    {
        dbHeadPtr->dbNextNodePtr = node;
        dbTailPtr->dbPreviousNodePtr = node;
    }
    else if ( headOrTail == HEAD )
    {
        (dbHeadPtr->dbNextNodePtr)->dbPreviousNodePtr = node;
        
        node->dbNextNodePtr = dbHeadPtr->dbNextNodePtr;
        
        dbHeadPtr->dbNextNodePtr = node;
    }
    //headOrTail == TAIL
    else
    {
        // 상황  ...[노드]<->[마지막노드]<- tail(pre)       [새 노드]
        (dbTailPtr->dbPreviousNodePtr)->dbNextNodePtr = node;
        
        node->dbPreviousNodePtr = dbTailPtr->dbPreviousNodePtr;
        
        dbTailPtr->dbPreviousNodePtr = node;
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
    
    DbNode * selectNode = dbHeadPtr;   // 선택된 노드. 노드 주소를 이동 할때만 사용.
    int iDelNodeCount = 0;           // 삭제된 노드 갯수 카운트
    
    // 리스트를 처음부터 끝까지 검색
    while( selectNode != NULL ) // 있다가 selectNode == tail.next로 바꿔보깅 더블에서만 가능하니까
    {
        if( selectNode->iDbNodeData == delData )
        {
            // 노드가 1개 있을 경우  head -> [선택노드] <- tail
            if( dbHeadPtr->dbNextNodePtr == dbTailPtr->dbPreviousNodePtr )
            {
                dbHeadPtr->dbNextNodePtr = dbTailPtr->dbPreviousNodePtr = NULL ;
            }
            // 노드가 2개 이상 있을경우
            else
            {
                // 선택 노드가 첫번째 노드일 경우  head -> [선택노드] <-> [다음노드] ...
                if( selectNode->dbPreviousNodePtr == NULL ) // dbHeadPtr->dbNextNodePtr == selectNode 요것도 되겠네
                {
                    dbHeadPtr->dbNextNodePtr = selectNode->dbNextNodePtr ;
                    (selectNode->dbNextNodePtr)->dbPreviousNodePtr = NULL;
                   // free(selectNode); 머리 꼬리 다 확인하고 해야겠다
                }
                // 선택 노드가 마지막 노드일 경우   [앞노드] <-> [선택노드] <- tail
                else if ( selectNode->dbNextNodePtr == NULL )
                {
                    dbTailPtr->dbPreviousNodePtr = selectNode->dbPreviousNodePtr ;
                    (selectNode->dbPreviousNodePtr)->dbNextNodePtr = NULL;
                }
                // 선택 노드가 첫번째도 마지막도 아님.
                else
                {
                    // 앞노드의 dbNextNodePtr(다음노드주소값)을 뒷노드로 바꿈.
                    (selectNode->dbPreviousNodePtr)->dbNextNodePtr = selectNode->dbNextNodePtr ;
                    (selectNode->dbNextNodePtr)->dbPreviousNodePtr = selectNode->dbPreviousNodePtr;
                }
            }
            free(selectNode);
            iDelNodeCount++;
        }
        selectNode = selectNode->dbNextNodePtr ;
    }
    printf(" ~ 삭제된 노드의 갯수 : %d \n\n",iDelNodeCount);
    
    return TRUE ;
}