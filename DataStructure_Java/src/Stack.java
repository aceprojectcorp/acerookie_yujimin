public class Stack extends SingleLinkedList
{	
	public void DelSLL( NodeForSingleLinked top )
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
				top.next = top.next.next;	// top -> [ù���] -> [�ι�°���] ... 
			}			
		}		
		System.out.println();		
	}	
}
