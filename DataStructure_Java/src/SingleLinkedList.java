public class SingleLinkedList 
{
	// 노드 추가 // 항상 리스트의 맨 앞부분에 추가됨 (head가 가르키는 노드의 앞)	
	public void AddSLL( NodeForSingleLinked head, int input )
	{
		System.out.println("* 데이터 추가 : "+input);
		
		NodeForSingleLinked node = new NodeForSingleLinked( input );
		
		// 리스트에 노드가 존재  
		if( head.next != null )
		{
			node.next = head.next ; 			
		}
		head.next = node;
		
		System.out.println();
	}
	
	// 모든 노드 출력 
	public void Show( NodeForSingleLinked head )
	{
		System.out.println("* 모든 노드 출력");
		
		if( head.next == null )
		{
			System.out.println("리스트가 비어있음");
		}
		else
		{
			NodeForSingleLinked selectNode = head.next;
			
			while( selectNode != null )
			{
				System.out.print( selectNode.num + "\t" );
				selectNode = selectNode.next;			
			}			
		}
		
		System.out.println("\n");
	}
	
	// 노드삭제 
	public void DelSLL( NodeForSingleLinked head, int input )
	{
		System.out.println("* 데이터 값이 " + input + "인 노드 모두 삭제");
		
		if( head.next == null )
		{
			System.out.println(" ~ 삭제할 노드가 없습니다.");
		}
		else
		{
			NodeForSingleLinked selectNode = head.next;
			NodeForSingleLinked frontNode = null;
			
			while( selectNode != null )
			{
				if( selectNode.num == input )
				{
					// 리스트에 노드가 단 하나 뿐일 때
					if( selectNode == null && selectNode == head.next )
					{
						head.next = null ; 
					}						
					// 맨 앞 노드일 때
					else if( selectNode == head.next )
					{
						head.next = head.next.next;
					}					
					// 맨 뒤 노드일 때 
					else if( selectNode.next == null )
					{
						frontNode.next = null;
					}
					else
					{
						frontNode.next = selectNode.next;
					}					
				}
				
				// break; // 1개의 데이터만 지우고자 할 때
				frontNode = selectNode;
				selectNode = selectNode.next;				
			}
		}
		
		System.out.println();
	}
}
