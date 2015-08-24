class SLLNode
{
	int num ; 
	SLLNode next;
	
	public SLLNode( int a )
	{
		num = a;
		next = null;
	}
}


class SList
{
	// 노드 추가는 항상 리스트의 맨 앞부분에 추가됨 (head가 가르키는 노드의 앞)
	
	// 노드 추가 
	public void AddSLL( SLLNode head, int input )
	{
		System.out.println("* 데이터 추가 : "+input);
		
		SLLNode node = new SLLNode( input );
		
		// 리스트에 노드가 존재  
		if( head.next != null )
		{
			node.next = head.next ; 			
		}
		head.next = node;
		
		System.out.println();
	}
	
	// 모든 노드 출력 
	public void ShowSLL( SLLNode head )
	{
		System.out.println("* 단일 연결 리스트의 모든 노드 출력");
		
		if( head.next == null )
		{
			System.out.println("리스트가 비어있음");
		}
		else
		{
			SLLNode selectNode = head.next;
			
			while( selectNode != null )
			{
				System.out.print( selectNode.num + "\t" );
				selectNode = selectNode.next;			
			}			
		}
		
		System.out.println("\n");
	}
	
	// 노드삭제 
	public void DelSLL( SLLNode head, int input )
	{
		System.out.println("* 데이터 값이 " + input + "인 노드 모두 삭제");
		
		if( head.next == null )
		{
			System.out.println(" ~ 삭제할 노드가 없습니다.");
		}
		else
		{
			SLLNode selectNode = head.next;
			SLLNode frontNode = null;
			
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
public class SingleLinkedList {

	public static void main(String[] args) {
		
		// head생성
		SLLNode SLLhead = new SLLNode( 0 );
		
		// 리스트 생성 
		SList sl = new SList();
		
		sl.AddSLL( SLLhead, 1 );
		sl.AddSLL( SLLhead, 2 );
		sl.AddSLL( SLLhead, 3 );
		sl.AddSLL( SLLhead, 2 );
		sl.ShowSLL( SLLhead );
		
		sl.DelSLL( SLLhead, 2 );
		sl.ShowSLL( SLLhead );
		
		sl.DelSLL( SLLhead, 1 );
		sl.ShowSLL( SLLhead );
		
		sl.DelSLL( SLLhead, 3 );
		sl.DelSLL( SLLhead, 3 );
		
		sl.ShowSLL( SLLhead );
		
	}
}
