
public class DataStructureMain {

	public static void main(String[] args) {
		
		// ---- 단일연결리스트 테스트 코드 시작 ---- 
		System.out.println(" *** 단일 연결 리스트 테스트 코드 시작 *** ");
		
		// head생성
		NodeForSingleLinked SLLhead = new NodeForSingleLinked( );
				
		// 리스트 생성 
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
		
		
		//----- stack 테스트 코드 시작 ---------------
		System.out.println("\n\n *** 스택 테스트 코드 시작 *** ");
		// head생성
		NodeForSingleLinked top = new NodeForSingleLinked();
		
		// 스택 생성
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
		
		
		//----- 이중연결리스트 테스트 코드 시작 -------
		System.out.println("\n\n *** 이중 연결 리스트 테스트 코드 시작 *** ");
		// head생성
		NodeForDoubleLinked DLLhead = new NodeForDoubleLinked();
		// tail생성
		NodeForDoubleLinked DLLtail = new NodeForDoubleLinked();
		// 리스트 생성
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
		
		
		// ----- 큐 테스트 코드 시작 --------------
		System.out.println("\n\n *** 큐 테스트 코드 시작 *** ");
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
