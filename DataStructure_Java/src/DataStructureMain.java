
public class DataStructureMain {

	public static void main(String[] args) {
		
		// ---- ���Ͽ��Ḯ��Ʈ �׽�Ʈ �ڵ� ���� ---- 
		System.out.println(" *** ���� ���� ����Ʈ �׽�Ʈ �ڵ� ���� *** ");
		
		// head����
		NodeForSingleLinked SLLhead = new NodeForSingleLinked( );
				
		// ����Ʈ ���� 
		SingleLinkedList sl = new SingleLinkedList();
		
		sl.AddSLL( SLLhead, 1 );
		sl.AddSLL( SLLhead, 2 );
		sl.AddSLL( SLLhead, 3 );
		sl.AddSLL( SLLhead, 2 );
		sl.Show( SLLhead );
				
		sl.DelSLL( SLLhead, 2 );
		sl.Show( SLLhead );
		
		sl.DelSLL( SLLhead, 1 );
		sl.Show( SLLhead );
				
		sl.DelSLL( SLLhead, 3 );
		sl.DelSLL( SLLhead, 3 );
				
		sl.Show( SLLhead );
		//------------------------------------
		
		
		//----- stack �׽�Ʈ �ڵ� ���� ---------------
		System.out.println("\n\n *** ���� �׽�Ʈ �ڵ� ���� *** ");
		// head����
		NodeForSingleLinked top = new NodeForSingleLinked();
		
		// ���� ����
		Stack stack = new Stack();

		stack.AddSLL(top, 1);		
		stack.AddSLL(top, 2);
		stack.AddSLL(top, 3);

		stack.Show(top);
		
		stack.DelSLL(top);
		
		stack.Show(top);
		
		stack.DelSLL(top);
		stack.DelSLL(top);
		stack.DelSLL(top);
		
		stack.Show(top);
		//-------------------------------------
		
		
		//----- ���߿��Ḯ��Ʈ �׽�Ʈ �ڵ� ���� -------
		System.out.println("\n\n *** ���� ���� ����Ʈ �׽�Ʈ �ڵ� ���� *** ");
		// head����
		NodeForDoubleLinked DLLhead = new NodeForDoubleLinked();
		// tail����
		NodeForDoubleLinked DLLtail = new NodeForDoubleLinked();
		// ����Ʈ ����
		DoubleLinkedList dlist = new DoubleLinkedList();
		
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
		//-------------------------------------
		
		
		// ----- ť �׽�Ʈ �ڵ� ���� --------------
		System.out.println("\n\n *** ť �׽�Ʈ �ڵ� ���� *** ");
		NodeForDoubleLinked QueueHead = new NodeForDoubleLinked();
		NodeForDoubleLinked QueueTail = new NodeForDoubleLinked();
		Queue queue = new Queue();
		
		queue.DLLAdd( QueueHead, QueueTail, 1 );
		queue.DLLAdd( QueueHead, QueueTail, 2 );
		queue.DLLAdd( QueueHead, QueueTail, 3 );
		
		queue.DLLShow(QueueHead, QueueTail);
		
		queue.PopFromQueue(QueueHead, QueueTail);
		
		queue.DLLShow(QueueHead, QueueTail);
		
		queue.PopFromQueue(QueueHead, QueueTail);
		queue.DLLShow(QueueHead, QueueTail);
		
		queue.PopFromQueue(QueueHead, QueueTail);
		queue.PopFromQueue(QueueHead, QueueTail);
		
		queue.DLLShow(QueueHead, QueueTail);
		//--------------------------------------
		
	}

}
