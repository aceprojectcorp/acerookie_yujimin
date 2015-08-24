class StNode
{
	int num ; 
	StNode next;
	
	public StNode( int a )
	{
		num = a;
		next = null;
	}
}

class StList
{
	public void PushToStack( StNode top, int input )
	{
		System.out.println("* PUSH : "+input);
		
		StNode node = new StNode( input );
		
		// 리스트에 노드가 존재  
		if( top.next != null )
		{
			node.next = top.next ; 			
		}
		top.next = node;
		
		System.out.println();
		
	}
	public void ShowStack( StNode top )
	{
		System.out.println("* 스택의 모든 노드 출력");
		
		if( top.next == null )
		{
			System.out.println(" ~ 리스트가 비어있음");
		}
		else
		{
			StNode selectNode = top.next;
			
			while( selectNode != null )
			{
				System.out.println( selectNode.num + "\t" );
				selectNode = selectNode.next;			
			}			
		}
		
		System.out.println("\n");
	
	}
	
	public void PopFromStack( StNode top )
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
				top.next = top.next.next;
			}			
		}
		
		System.out.println();		
	}	
}
public class Stack {

	public static void main(String[] args) {
		
		// head생성
		StNode top = new StNode( 0 );
		
		// 스택 생성
		StList stack = new StList();
		
		stack.PushToStack(top, 1);
		stack.PushToStack(top, 2);
		stack.PushToStack(top, 3);
		
		stack.ShowStack(top);
		
		stack.PopFromStack(top);
		
		stack.ShowStack(top);
		
		stack.PopFromStack(top);
		stack.PopFromStack(top);
		stack.PopFromStack(top);
		
		stack.ShowStack(top);		

	}
}
