// tail -> [���� �ֱٿ� ���� ���] <-> [���] <->.. [���] <-> [���� ó���� ���� ���] <- head
// PUSH�� ��� tail�������� ��� �߰���.
// POP�� ��� head������ ��尡 ������. (���� ó���� ���� ������ ����)
public class Queue extends DoubleLinkedList
{		
	public void PopFromQueue( NodeForDoubleLinked head , NodeForDoubleLinked tail )
	{
		System.out.print("* POP : "  );
		
		if( head.pre == null )
		{
			System.out.println(" ~ ������ ��尡 �����ϴ�.");
		}
		else
		{
			System.out.println( head.pre.num );
			// ���ÿ� ��尡 �� �ϳ� ���� �� 
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
