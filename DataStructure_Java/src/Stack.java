public class Stack extends SingleLinkedList
{	
	public void DelSLL( NodeForSingleLinked top )
	{
		System.out.print("* POP : ");
		
		if( top.next == null )
		{
			System.out.println(" ~ 삭제할 노드가 없습니다.");
		}
		else
		{
			System.out.println( top.next.num );
			// 스택에 노드가 단 하나 뿐일 때 
			if( top.next.next == null )
			{
				top.next = null;
			}
			else
			{
				top.next = top.next.next;	// top -> [첫노드] -> [두번째노드] ... 
			}			
		}		
		System.out.println();		
	}	
}
