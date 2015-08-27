// tail -> [가장 최근에 들어온 노드] <-> [노드] <->.. [노드] <-> [제일 처음에 들어온 노드] <- head
// PUSH할 경우 tail방향으로 노드 추가됨.
// POP할 경우 head방향의 노드가 삭제됨. (제일 처음에 들어온 노드부터 삭제)
public class Queue extends DoubleLinkedList
{		
	public void PopFromQueue( NodeForDoubleLinked head , NodeForDoubleLinked tail )
	{
		System.out.print("* POP : "  );
		
		if( head.pre == null )
		{
			System.out.println(" ~ 삭제할 노드가 없습니다.");
		}
		else
		{
			System.out.println( head.pre.num );
			// 스택에 노드가 단 하나 뿐일 때 
			if( head.pre.pre == null )
			{
				head.pre = null;
				tail.next = null;
			}
			else
			{
				head.pre = head.pre.pre; 
				head.pre.next = null;
			}			
		}
		
		System.out.println();		
	}	

}
