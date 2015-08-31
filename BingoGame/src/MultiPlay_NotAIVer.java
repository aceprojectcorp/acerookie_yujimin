public class MultiPlay_NotAIVer extends SoloPlay {
	 
	BingoGrid comBG ;	// 컴 빙고판 객체   
	int comSuccLineNum;	// 컴이 성공한 빙고 라인 수 
	
	public MultiPlay_NotAIVer()
	{
		resetBingo();				
	}		
	
	void resetBingo()
	{
		myBG = new BingoGrid( BINGOSIZE );
		comBG = new BingoGrid( BINGOSIZE );
		mySuccLineNum = 0;			
		comSuccLineNum = 0; 
		playTime = 0 ;		
	}
	
	// 빙고판 출력 - 입력된 빙고 번호의 체크까지 함께 출력 
	// arr 숫자값 저장한 배열 / arr2 체크값 저장한 배열 / whosBingoFan가 0이면 나, 아니면 컴퓨터 
	void printBG( BingoGrid bg, int whosBingoFan )
	{		
		 if( whosBingoFan == 0 ) 
			System.out.println("\n============ << 내 빙고판 >> ==============");
		 else
			System.out.println( "\n============ << 상대 빙고판 >> =============");
		 
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				if( bg.checkGrid[i][j] == 1 )
				{
					System.out.print( "(" + bg.numGrid[i][j] + ")\t" );
				}
				else
				{
					if(  whosBingoFan == 0 || whosBingoFan == 1)
						System.out.print( bg.numGrid[i][j] +"\t" );
				}
				
			}	System.out.println();		
		}
		System.out.println("=========================================");		
	}
	
	//play중 출력부분. 게임횟수, 내 숫자빙고판, 상대 숫자빙고판, 완성 빙고라인수, 이전 턴의 입력값들 출력. 
	void printPlayingInfo( int playTime, int inputMyNum, int inputComNum, boolean myTurnGood, boolean comTurnGood )
	{
		if( playTime > 1 )
		{				
			System.out.println("\n\n**************************** [ " + playTime + "판째 ] ****");
		}	
		
		// 출력 
		printBG( myBG, 0 );
		
		if( playTime >= 2 )
		{
			System.out.println("* 완성된 나의 빙고 수 : " + mySuccLineNum ) ;
		}		
		
		printBG( comBG, 1 );
		if( playTime >= 2 )
		{
			System.out.println("* 완성된 상대의 빙고 수 : " + comSuccLineNum ) ;
				
			if( myTurnGood == false )
			{
				System.out.println(" ~ 내가 입력한 숫자 " + inputMyNum + "은 상대에게 없는 숫자 였습니다. ^_^");
				myTurnGood = true;				
			}
			else
			{
				System.out.println(" ~ 내가 입력한 숫자 " + inputMyNum + "은 상대에게 있는 숫자 였습니다. T-T");
			}
			
			if( comTurnGood == false )
			{
				System.out.println(" ~ 컴퓨터가 입력한 숫자 " + inputComNum + "은 나에게 없는 숫자 였습니다. T-T ");
				comTurnGood = true;				
			}
			else
			{
				System.out.println(" ~ 컴퓨터가 입력한 숫자 " + inputComNum + "은 나에게 있는 숫자 였습니다. ^_^ ");
			}
		}
	}
	
	// 게임 종료 관련 메세지 출력
	void printEnd( int inputMyNum, int inputComNum, int comSuccLineNum )
	{		
		System.out.println("\n\n******************[끝]********************\n ~ 마지막에 내가 입력한 숫자 " + inputMyNum);
		System.out.println(" ~ 마지막에 컴이 입력한 숫자 " + inputComNum);
		
		// 게임 끝남. 
		printBG( myBG, 0 );
		printBG( comBG, 1 );
		System.out.println( "* 성공 빙고줄 수_나 : " + mySuccLineNum+"\n* 성공 빙고줄 수_컴 : " + comSuccLineNum );
		
		
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
	
	
	// 실질적 게임 플레이 반복 함수. 모든 플레이어의 완성된 빙고라인수가 4개 이하인 경우 계속 반복됨. 
	void play()
	{	
		boolean myTurnGood = true;
		boolean comTurnGood = true;
		int inputMyNum = 0;				// 내가 입력한 숫자값 
		int inputComNum = 0;			// 컴퓨터가 입력한 숫자값  
		
		// 게임반복 구간 
		while( mySuccLineNum < BINGOSIZE && comSuccLineNum < BINGOSIZE ) 
		{	
			++playTime;
			
			// 한턴에서 출력할 모든 내용 출력. 
			printPlayingInfo( playTime, inputMyNum, inputComNum, myTurnGood, comTurnGood );
			
			// ** 내 턴 시작 
			inputMyNum = isValidNum( myBG );
			myBG.addCheckToCheckGrid(inputMyNum);
			myTurnGood =  comBG.hasCheckNum( inputMyNum ); 			
			
			// 상대가 시작 하기 전에 내 성공 여부 확인 
			mySuccLineNum = myBG.allSuccBgLine(); 
			if( mySuccLineNum >= BINGOSIZE || comSuccLineNum >= BINGOSIZE )
				break;
			
			// ** 상대 턴 시작 	
			inputComNum = comBG.randCheck();
			
			// 내 빙고판에 해당하는 숫자가 있으면 내 빙고판에 체크, 해당 숫자의 존재 유무 결과를 comTurnGood에 저장.
			comTurnGood = myBG.hasCheckNum( inputComNum );
		
			mySuccLineNum = myBG.allSuccBgLine();
			comSuccLineNum = comBG.allSuccBgLine(); 			
		}		
		printEnd( inputMyNum, inputComNum, comSuccLineNum );	// 게임 종료 관련 메세지 출력		
	}	

}
