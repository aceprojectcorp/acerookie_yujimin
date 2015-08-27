
public class DoubleLinkedList 
{
	// 노드 삽입 
	public void DLLAdd( NodeForDoubleLinked head, NodeForDoubleLinked tail, int input )
	{
		System.out.println("* 데이터 추가 : "+input);
		
		NodeForDoubleLinked node = new NodeForDoubleLinked( input );
		
		// 리스트에 노드가 존재  
		if( tail.next != null )
		{
			node.next = tail.next ; 	// [새 노드] -> [첫번째노드] <= tail
			tail.next.pre = node; 		// [새 노드] <- [첫번째노드] <= tail 
		}
		else
		{
			head.pre = node;
		}
		tail.next = node;
		
		System.out.println();
	}
	
	// 리스트내 모든 노드 출력 
	public void DLLShow( NodeForDoubleLinked head, NodeForDoubleLinked tail )
	{
		System.out.println("* 모든 노드 출력");
		
		if( tail.next == null )
		{
			System.out.println("리스트가 비어있음");
			
		}
		else
		{
			NodeForDoubleLinked selectNode = tail.next;
			
			while( selectNode != null )
			{
				System.out.print( selectNode.num + "\t" );
				selectNode = selectNode.next;			
			}			
		}
		
		System.out.println("\n");
		
	}
	
	// 노드 삭제 
	public void DLLDel( NodeForDoubleLinked head, NodeForDoubleLinked tail, int input ) 
	{
		System.out.println("* 데이터 값이 " + input + "인 노드 모두 삭제");
		
		if( tail.next == null )
		{
			System.out.println(" ~ 삭제할 노드가 없습니다.");
		}
		else
		{
			NodeForDoubleLinked selectNode = tail.next;

			// 모든 노드 찾기 
			while( selectNode != null )
			{
				if( selectNode.num == input )
				{
					// 리스트에 노드가 단 하나 뿐일때
					if( selectNode == null && selectNode == tail.next )
					{
						tail.next = head.next = null ; 
					}						
					// 맨 앞 노드일 때	// tail -> [선택노드] <-> [노드] <->...
					else if( selectNode == tail.next )
					{
						tail.next = tail.next.next;
					}					
					// 맨 뒤 노드일 때 	// ...[노드] <-> [선택노드] <- head
					else if( selectNode.next == null )
					{
						selectNode.pre.next = null;	
						head.next = selectNode.pre;
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
