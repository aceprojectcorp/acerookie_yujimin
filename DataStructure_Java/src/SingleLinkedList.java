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
	// ��� �߰��� �׻� ����Ʈ�� �� �պκп� �߰��� (head�� ����Ű�� ����� ��)
	
	// ��� �߰� 
	public void AddSLL( SLLNode head, int input )
	{
		System.out.println("* ������ �߰� : "+input);
		
		SLLNode node = new SLLNode( input );
		
		// ����Ʈ�� ��尡 ����  
		if( head.next != null )
		{
			node.next = head.next ; 			
		}
		head.next = node;
		
		System.out.println();
	}
	
	// ��� ��� ��� 
	public void ShowSLL( SLLNode head )
	{
		System.out.println("* ���� ���� ����Ʈ�� ��� ��� ���");
		
		if( head.next == null )
		{
			System.out.println("����Ʈ�� �������");
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
	
	// ������ 
	public void DelSLL( SLLNode head, int input )
	{
		System.out.println("* ������ ���� " + input + "�� ��� ��� ����");
		
		if( head.next == null )
		{
			System.out.println(" ~ ������ ��尡 �����ϴ�.");
		}
		else
		{
			SLLNode selectNode = head.next;
			SLLNode frontNode = null;
			
			while( selectNode != null )
			{
				if( selectNode.num == input )
				{
					// ����Ʈ�� ��尡 �� �ϳ� ���� ��
					if( selectNode == null && selectNode == head.next )
					{
						head.next = null ; 
					}						
					// �� �� ����� ��
					else if( selectNode == head.next )
					{
						head.next = head.next.next;
					}					
					// �� �� ����� �� 
					else if( selectNode.next == null )
					{
						frontNode.next = null;
					}
					else
					{
						frontNode.next = selectNode.next;
					}					
				}
				
				// break; // 1���� �����͸� ������� �� ��
				frontNode = selectNode;
				selectNode = selectNode.next;				
			}
		}
		
		System.out.println();
	}
}
public class SingleLinkedList {

	public static void main(String[] args) {
		
		// head����
		SLLNode SLLhead = new SLLNode( 0 );
		
		// ����Ʈ ���� 
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
