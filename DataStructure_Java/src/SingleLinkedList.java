public class SingleLinkedList 
{
	// ��� �߰� // �׻� ����Ʈ�� �� �պκп� �߰��� (head�� ����Ű�� ����� ��)	
	public void AddSLL( NodeForSingleLinked head, int input )
	{
		System.out.println("* ������ �߰� : "+input);
		
		NodeForSingleLinked node = new NodeForSingleLinked( input );
		
		// ����Ʈ�� ��尡 ����  
		if( head.next != null )
		{
			node.next = head.next ; 			
		}
		head.next = node;
		
		System.out.println();
	}
	
	// ��� ��� ��� 
	public void Show( NodeForSingleLinked head )
	{
		System.out.println("* ��� ��� ���");
		
		if( head.next == null )
		{
			System.out.println("����Ʈ�� �������");
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
	
	// ������ 
	public void DelSLL( NodeForSingleLinked head, int input )
	{
		System.out.println("* ������ ���� " + input + "�� ��� ��� ����");
		
		if( head.next == null )
		{
			System.out.println(" ~ ������ ��尡 �����ϴ�.");
		}
		else
		{
			NodeForSingleLinked selectNode = head.next;
			NodeForSingleLinked frontNode = null;
			
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
