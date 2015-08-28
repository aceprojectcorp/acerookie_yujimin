import java.util.*;
import java.io.IOException;

public class MultiPlay_NotAIVer extends SoloPlay {

	public final static int BINGOSIZE = 5 ;
	
	int[][] comBingFan ;	// 컴 빙고판 (숫자저장)
	int[][] comBingFan2 ;	// 컴 빙고판 (체크저장)	
	int comSuccLineNum;		// 컴이 성공한 빙고 라인 수 
	
	public MultiPlay_NotAIVer()
	{
		resetBingo();				
	}		
	
	void resetBingo()
	{
		playTime = 0 ;
		mySuccLineNum = 0 ;
		comSuccLineNum = 0 ;
		myBingFan = new int[BINGOSIZE][BINGOSIZE];
		myBingFan2 = new int[BINGOSIZE][BINGOSIZE];
		comBingFan = new int[BINGOSIZE][BINGOSIZE];
		comBingFan2 = new int[BINGOSIZE][BINGOSIZE];
		
		// 빙고판1,2 초기화   
		resetBingoFan( myBingFan );
		resetBingoFan( myBingFan2 );
		resetBingoFan( comBingFan );
		resetBingoFan( comBingFan2 );
		
		// 플레이어와 컴퓨터의 기본 숫자 빙고판 초기화(만들기) . 
		createBingoFan( myBingFan );
		createBingoFan( comBingFan );
	}
	
	// 빙고판 출력 - 입력된 빙고 번호의 체크까지 함께 출력 
	// arr 숫자값 저장한 배열 / arr2 체크값 저장한 배열 / whosBingoFan가 0이면 나, 아니면 컴퓨터 
	void printBingoFan( int[][] arr, int[][] arr2, int whosBingoFan )
	{		
		 if( whosBingoFan == 0 ) 
			System.out.println("\n============ << 내 빙고판 >> ==============");
		 else
			System.out.println( "\n============ << 상대 빙고판 >> =============");
		 
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				if( arr2[i][j] == 1 )
				{
					System.out.print( "(" + arr[i][j] + ")\t" );
				}
				else
				{
					if(  whosBingoFan == 0 || whosBingoFan == 1)
						System.out.print( arr[i][j] +"\t" );
				}
				
			}	System.out.println();		
		}
		System.out.println("=========================================");		
	}
	
	// 실질적 게임 플레이 반복 함수. 모든 플레이어의 완성된 빙고라인수가 4개 이하인 경우 계속 반복됨. 
	void play()
	{	
		boolean myTurnGood = true;
		boolean comTurnGood = true;
		int inputNum = 0;				// 내가 입력한 숫자값 
		int inputCom = 0;				// 컴퓨터가 입력한 숫자값  
		Random rd = new Random() ;
		
		// 게임반복 구간 
		while( mySuccLineNum < BINGOSIZE && comSuccLineNum < BINGOSIZE ) 
		{	
			if( playTime >= 1 )
			{				
				System.out.println("\n\n**************************** [ " + playTime + "판째 ] ****");
			}
			
			++playTime;
			
			// 출력 
			printBingoFan( myBingFan, myBingFan2, 0 );
			
			if( playTime >= 2 )
			{
				System.out.println("* 완성된 나의 빙고 수 : " + mySuccLineNum ) ;
			}		
			
			printBingoFan( comBingFan, comBingFan2, 1 );
			if( playTime >= 2 )
			{
				System.out.println("* 완성된 상대의 빙고 수 : " + comSuccLineNum ) ;
					
				if( myTurnGood == false )
				{
					System.out.println(" ~ 내가 입력한 숫자 " + inputNum + "은 상대에게 없는 숫자 였습니다. ^_^");
					myTurnGood = true;				
				}
				else
				{
					System.out.println(" ~ 내가 입력한 숫자 " + inputNum + "은 상대에게 있는 숫자 였습니다. T-T");
				}
				
				if( comTurnGood == false )
				{
					System.out.println(" ~ 컴퓨터가 입력한 숫자 " + inputCom + "은 나에게 없는 숫자 였습니다. T-T ");
					comTurnGood = true;				
				}
				else
				{
					System.out.println(" ~ 컴퓨터가 입력한 숫자 " + inputCom + "은 나에게 있는 숫자 였습니다. ^_^ ");
				}
			}
			
			// ** 내 턴 시작 
			inputNum = isValidNum();
			checkBingoFan2( myBingFan, myBingFan2, inputNum );	
			myTurnGood = checkBingoFan2( comBingFan, comBingFan2, inputNum );
			
			// 상대가 시작 하기 전에 내 성공 여부 확인 
			mySuccLineNum = checkSuccLine( myBingFan2 );
			if( mySuccLineNum >= BINGOSIZE )
				break;
			
			// ** 상대 턴 시작 			
			// 컴빙고판2가 0인곳 랜덤으로 좌표(i,j) 찾고, 체크.  
				// 난수값 생성
			int[] tmp = { 0, 0 };
			while( true )
			{
				tmp[0] = Math.abs((rd.nextInt()) % BINGOSIZE ) ; // 0~4 사이의 난수 
				tmp[1] = Math.abs((rd.nextInt()) % BINGOSIZE ) ;
				
				// 해당 위치의 빙고 숫자가 체크되지 않은 상태면 
				if( comBingFan2[tmp[0]][tmp[1]] == 0 )
				{
					inputCom = comBingFan[tmp[0]][tmp[1]] ;
					
					// 내 빙고판에 해당하는 숫자가 있으면 내 빙고판에 체크, 해당 숫자의 존재 유무 결과를 comTurnGood에 저장.
					comTurnGood = checkBingoFan2( myBingFan, myBingFan2, inputCom );
					
					// 컴퓨터 빙고판에 체크.
					comBingFan2[tmp[0]][tmp[1]] = 1 ;				
					
					break;					
				}
			}
			
			mySuccLineNum = checkSuccLine( myBingFan2 );
			comSuccLineNum = checkSuccLine( comBingFan2 ); 
			
		}		
		
		System.out.println("\n\n******************[끝]********************\n ~ 마지막에 내가 입력한 숫자 " + inputNum);
		System.out.println(" ~ 마지막에 컴이 입력한 숫자 " + inputCom);
		
		// 게임 끝남. 
		printBingoFan( myBingFan, myBingFan2, 0 );
		printBingoFan( comBingFan, comBingFan2, 1 );
		System.out.println( "* 성공 빙고줄 수_나 : " + mySuccLineNum+"\n* 성공 빙고줄 수_컴 : "+comSuccLineNum );
		
		
		if( mySuccLineNum >= BINGOSIZE && comSuccLineNum >= BINGOSIZE )
		{
			System.out.println("\n\n!!! --- 비 김 --- !!!\n\n");
		}
		else if( mySuccLineNum == BINGOSIZE )
		{
			System.out.println("\n\n!!! --- 승 리 --- !!!\n\n");
		}
		else
		{			
			System.out.println("\n\n!!! --- 패 배 --- !!!\n\n");
		}
		
	}
	
	
	
	// 빙고판1에 체크된 값을 빙고판2에 저장.
	boolean checkBingoFan2( int[][] arr, int[][] arr2, int input )
	{
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				if( arr[i][j] == input )
				{
					arr2[i][j] = 1;
					return true;
				}
			}			
		}	
		return false;
	}
	

	
	

}
