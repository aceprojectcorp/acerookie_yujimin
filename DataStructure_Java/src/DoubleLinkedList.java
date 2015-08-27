
public class DoubleLinkedList 
{
	// ��� ���� 
	public void DLLAdd( NodeForDoubleLinked head, NodeForDoubleLinked tail, int input )
	{
		System.out.println("* ������ �߰� : "+input);
		
		NodeForDoubleLinked node = new NodeForDoubleLinked( input );
		
		// ����Ʈ�� ��尡 ����  
		if( tail.next != null )
		{
			node.next = tail.next ; 	// [�� ���] -> [ù��°���] <= tail
			tail.next.pre = node; 		// [�� ���] <- [ù��°���] <= tail 
		}
		else
		{
			head.pre = node;
		}
		tail.next = node;
		
		System.out.println();
	}
	
	// ����Ʈ�� ��� ��� ��� 
	public void DLLShow( NodeForDoubleLinked head, NodeForDoubleLinked tail )
	{
		System.out.println("* ��� ��� ���");
		
		if( tail.next == null )
		{
			System.out.println("����Ʈ�� �������");
			
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
	
	// ��� ���� 
	public void DLLDel( NodeForDoubleLinked head, NodeForDoubleLinked tail, int input ) 
	{
		System.out.println("* ������ ���� " + input + "�� ��� ��� ����");
		
		if( tail.next == null )
		{
			System.out.println(" ~ ������ ��尡 �����ϴ�.");
		}
		else
		{
			NodeForDoubleLinked selectNode = tail.next;

			// ��� ��� ã�� 
			while( selectNode != null )
			{
				if( selectNode.num == input )
				{
					// ����Ʈ�� ��尡 �� �ϳ� ���϶�
					if( selectNode == null && selectNode == tail.next )
					{
						tail.next = head.next = null ; 
					}						
					// �� �� ����� ��	// tail -> [���ó��] <-> [���] <->...
					else if( selectNode == tail.next )
					{
						tail.next = tail.next.next;
					}					
					// �� �� ����� �� 	// ...[���] <-> [���ó��] <- head
					else if( selectNode.next == null )
					{
						selectNode.pre.next = null;	
						head.next = selectNode.pre;
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
