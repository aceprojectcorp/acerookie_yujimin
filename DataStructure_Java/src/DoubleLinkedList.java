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
	// ��� ���� 
	public void DLLAdd( DLLNode head, DLLNode tail, int input )
	{
		System.out.println("* ������ �߰� : "+input);
		
		DLLNode node = new DLLNode( input );
		
		// ����Ʈ�� ��尡 ����  
		if( head.next != null )
		{
			node.next = head.next ; 	// [�� ���] -> [ù��°���] <= head
			head.next.pre = node; 		// [�� ���] <- [ù��°���] <= head 
		}
		else
		{
			tail.pre = node;
		}
		head.next = node;
		
		System.out.println();
	}
	
	// ����Ʈ�� ��� ��� ��� 
	public void DLLShow( DLLNode head, DLLNode tail )
	{
		System.out.println("* ���� ���� ����Ʈ�� ��� ��� ���");
		
		if( head.next == null )
		{
			System.out.println("����Ʈ�� �������");
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
	
	// ��� ���� 
	public void DLLDel( DLLNode head, DLLNode tail, int input ) 
	{
		System.out.println("* ������ ���� " + input + "�� ��� ��� ����");
		
		if( head.next == null )
		{
			System.out.println(" ~ ������ ��尡 �����ϴ�.");
		}
		else
		{
			DLLNode selectNode = head.next;

			// ��� ��� ã�� 
			while( selectNode != null )
			{
				if( selectNode.num == input )
				{
					// ����Ʈ�� ��尡 �� �ϳ� ���϶�
					if( selectNode == null && selectNode == head.next )
					{
						head.next = tail.next = null ; 
					}						
					// �� �� ����� ��
					else if( selectNode == head.next )
					{
						head.next = head.next.next;
					}					
					// �� �� ����� �� 
					else if( selectNode.next == null )
					{
						selectNode.pre.next = null;
						tail.next = selectNode.pre;
					}
					else
					{
						selectNode.pre.next = selectNode.next;	// [���]-[-��-��-��-��]->[���]
						selectNode.next.pre = selectNode.pre;	// [���]<-[-��-��-��-��]-[���]
					}					
				}
				
				// break; // 1���� �����͸� ������� �� ��
				selectNode = selectNode.next;				
			}
			
		}
		
		System.out.println();
	}
}

public class DoubleLinkedList {

	public static void main(String[] args) {

		// head����
		DLLNode DLLhead = new DLLNode( 0 );
		// tail����
		DLLNode DLLtail = new DLLNode( 0 );
		// ����Ʈ ����
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
