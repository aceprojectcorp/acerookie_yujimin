class DLLNode
{
	int num ; 
	DLLNode pre;
	DLLNode next;
	
	public DLLNode( int a )
	{
		num = a;
		next = null;
		pre = null;
	}
}

class DLList
{
	// 노드 삽입 
	public void DLLAdd( DLLNode head, DLLNode tail, int input )
	{
		System.out.println("* 데이터 추가 : "+input);
		
		DLLNode node = new DLLNode( input );
		
		// 리스트에 노드가 존재  
		if( head.next != null )
		{
			node.next = head.next ; 	// [새 노드] -> [첫번째노드] <= head
			head.next.pre = node; 		// [새 노드] <- [첫번째노드] <= head 
		}
		else
		{
			tail.pre = node;
		}
		head.next = node;
		
		System.out.println();
	}
	
	// 리스트내 모든 노드 출력 
	public void DLLShow( DLLNode head, DLLNode tail )
	{
		System.out.println("* 이중 연결 리스트의 모든 노드 출력");
		
		if( head.next == null )
		{
			System.out.println("리스트가 비어있음");
		}
		else
		{
			DLLNode selectNode = head.next;
			
			while( selectNode != null )
			{
				System.out.print( selectNode.num + "\t" );
				selectNode = selectNode.next;			
			}			
		}
		
		System.out.println("\n");
		
	}
	
	// 노드 삭제 
	public void DLLDel( DLLNode head, DLLNode tail, int input ) 
	{
		System.out.println("* 데이터 값이 " + input + "인 노드 모두 삭제");
		
		if( head.next == null )
		{
			System.out.println(" ~ 삭제할 노드가 없습니다.");
		}
		else
		{
			DLLNode selectNode = head.next;

			// 모든 노드 찾기 
			while( selectNode != null )
			{
				if( selectNode.num == input )
				{
					// 리스트에 노드가 단 하나 뿐일때
					if( selectNode == null && selectNode == head.next )
					{
						head.next = tail.next = null ; 
					}						
					// 맨 앞 노드일 때
					else if( selectNode == head.next )
					{
						head.next = head.next.next;
					}					
					// 맨 뒤 노드일 때 
					else if( selectNode.next == null )
					{
						selectNode.pre.next = null;
						tail.next = selectNode.pre;
					}
					else
					{
						selectNode.pre.next = selectNode.next;	// [노드]-[-선-택-노-드]->[노드]
						selectNode.next.pre = selectNode.pre;	// [노드]<-[-선-택-노-드]-[노드]
					}					
				}
				
				// break; // 1개의 데이터만 지우고자 할 때
				selectNode = selectNode.next;				
			}
			
		}
		
		System.out.println();
	}
}

public class DoubleLinkedList {

	public static void main(String[] args) {

		// head생성
		DLLNode DLLhead = new DLLNode( 0 );
		// tail생성
		DLLNode DLLtail = new DLLNode( 0 );
		// 리스트 생성
		DLList dlist = new DLList();
		
		dlist.DLLAdd( DLLhead, DLLtail, 1 );
		dlist.DLLAdd( DLLhead, DLLtail, 2 );
		dlist.DLLAdd( DLLhead, DLLtail, 3 );
		dlist.DLLAdd( DLLhead, DLLtail, 2 );
		dlist.DLLShow( DLLhead, DLLtail );
		
		dlist.DLLDel( DLLhead, DLLtail, 2 );
		dlist.DLLShow( DLLhead, DLLtail );
		
		dlist.DLLDel( DLLhead, DLLtail, 1 );
		dlist.DLLShow( DLLhead, DLLtail );
		
		dlist.DLLDel( DLLhead, DLLtail, 3 );
		dlist.DLLDel( DLLhead, DLLtail, 3 );
		
		dlist.DLLShow( DLLhead, DLLtail );

	}

}
