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
		
		// ����Ʈ�� ��尡 ����  
		if( top.next != null )
		{
			node.next = top.next ; 			
		}
		top.next = node;
		
		System.out.println();
		
	}
	public void ShowStack( StNode top )
	{
		System.out.println("* ������ ��� ��� ���");
		
		if( top.next == null )
		{
			System.out.println(" ~ ����Ʈ�� �������");
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
			System.out.println(" ~ ������ ��尡 �����ϴ�.");
		}
		else
		{
			System.out.println( top.next.num );
			// ���ÿ� ��尡 �� �ϳ� ���� �� 
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
		
		// head����
		StNode top = new StNode( 0 );
		
		// ���� ����
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
