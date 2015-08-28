
public class DoubleLinkedList 
{
	// ��� ���� 
	public void DLLAdd( NodeForDoubleLinked head, NodeForDoubleLinked tail, int input )
	{
		System.out.println("* ������ �߰� : "+input);
		
		NodeForDoubleLinked node = new NodeForDoubleLinked( input );
		
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
	public void DLLShow( NodeForDoubleLinked head, NodeForDoubleLinked tail )
	{
		System.out.println("* ��� ��� ���");
		
		if( head.next == null )
		{
			System.out.println("����Ʈ�� �������");
			
		}
		else
		{
			NodeForDoubleLinked selectNode = head.next;
			
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
		
		if( head.next == null )
		{
			System.out.println(" ~ ������ ��尡 �����ϴ�.");
		}
		else
		{
			NodeForDoubleLinked selectNode = head.next;

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
					// �� �� ����� ��	// head -> [���ó��] <-> [���] <->...
					else if( selectNode == head.next )
					{
						head.next = head.next.next;
					}					
					// �� �� ����� �� 	// ...[���] <-> [���ó��] <- tail
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
