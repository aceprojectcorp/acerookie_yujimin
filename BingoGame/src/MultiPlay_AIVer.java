import java.util.Random;


public class MultiPlay_AIVer extends BingoMulti2{
	int[][] myBingFan3 ;	// 내 빙고판 (우선순위 저장)	
	int[][] comBingFan3 ;	// 컴 빙고판 (우선순위 저장)	
	
	public MultiPlay_AIVer()
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
		myBingFan3 = new int[BINGOSIZE][BINGOSIZE];
		comBingFan = new int[BINGOSIZE][BINGOSIZE];
		comBingFan2 = new int[BINGOSIZE][BINGOSIZE];
		comBingFan3 = new int[BINGOSIZE][BINGOSIZE];
		
		// 빙고판1,2,3 초기화  
		resetBingoFan( myBingFan );
		resetBingoFan( myBingFan2 );
		resetBingoFan( myBingFan3 );
		resetBingoFan( comBingFan );
		resetBingoFan( comBingFan2 );
		resetBingoFan( comBingFan3 );
		
		createBingoFan( myBingFan );
		createBingoFan( comBingFan );
	}
	
	// 빙고판 초기화 
	void resetBingoFan( int[][] arr )
	{
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				arr[i][j] = 0;
			}			
		}		
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
			
			// test
			printBingoFan( myBingFan3, myBingFan2, 0);
			resetBingoFan( myBingFan3 );
			
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
		
			// test
			checkBingoFan3( myBingFan2, myBingFan3 );
				
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
			
			if( comTurnGood == true )
			{
				checkBingoFan3( myBingFan2, myBingFan3 );
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
	
	// 빙고판3 만들기. 우선순위 설정.  
	void checkBingoFan3( int[][] arr2, int[][] arr3 )
	{	
		boolean dL1 = false;	// 대각선 \ 방향을 확인해야 하는지 알려주는 변수 
		boolean dL2 = false;	// 대각선 / 방향을 확인해야 하는지 알려주는 변수 
		
		for( int i=0 ; i < BINGOSIZE ; i++ )
		{
			for( int j=0 ; j < BINGOSIZE ; j++ )
			{
				// 값이 존재하는 숫자의 가로,세로,대각선(조건이 맞을경우에만)의 방향에 위치해 있는 값에 우선순위를 높여줌 
				if( arr2[i][j] == 1 )
				{
					arr3[i][j] = 20 ; 	// 해당 위치에 값이 체크되어 있음을 의미함. 
					
					if( i == j )
						dL1 = true;
					
					if( i+j == ( BINGOSIZE-1 ))
						dL2 = true;
					
					for( int k=0 ; k < BINGOSIZE ; k++ )
					{
						arr3[i][k]++;		// 가로 
						arr3[k][j]++;		// 세로 
						
						if( dL1 == true )	// \
							arr3[k][k]++;
						
						if( dL2 == true)	// /
							arr3[ Math.abs( k - (BINGOSIZE-1)) ][k]++;										
					}
					 
					dL1 = false;
					dL2 = false;				
				}				
			}
		}
		
	}
	

}
