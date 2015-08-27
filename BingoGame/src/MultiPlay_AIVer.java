import java.util.Random;
import java.util.ArrayList;
import java.util.Scanner;

public class MultiPlay_AIVer extends BingoMulti2{

	int[][] myBingFan3 ;	// 내 빙고판 (우선순위 저장)
	int[][] comBingFan3 ;	// 컴 빙고판 (우선순위 저장)
	int gameLevel ;	

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

		gameLevel = 1; 		

		// 빙고판1,2,3 초기화  
		resetBingoFan( myBingFan );
		resetBingoFan( myBingFan2 );
		resetBingoFan3( myBingFan3 );
		resetBingoFan( comBingFan );
		resetBingoFan( comBingFan2 );
		resetBingoFan3( comBingFan3 );	

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

	void resetBingoFan3( int[][] arr )
	{		
		// 빙고판의 위치에 따른 우선순위값 설정.
		// 성공가능한 빙고 라인 갯수에 따라 설정함.		

		if( BINGOSIZE == 5 )
		{
			int tmpArr[][]

				= { {3, 2, 2, 2, 3},

					{3, 2, 2, 2, 3},

					{3, 2, 4, 2, 3},

					{3, 2, 2, 2, 3},

					{3, 2, 2, 2, 3}

					};	

			for( int i= 0 ; i < BINGOSIZE ; i++ )
			{
				for(int j = 0 ; j < BINGOSIZE ; j++)
				{
					arr[i][j] = tmpArr[i][j];
				}			
			}
		}

		else if( BINGOSIZE == 3 )
		{
			int tmpArr2[][]
					= { {3, 2, 3},
						{2, 4, 2},
						{3, 2, 3},
					  };	
				for( int i= 0 ; i < BINGOSIZE ; i++ )
				{
					for(int j = 0 ; j < BINGOSIZE ; j++)
					{
						arr[i][j] = tmpArr2[i][j];
					}
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

		// 난이도 입력 
		System.out.println("* 난이도를 선택해 주세요. \n 1. 쉬움\n 2. 어려움 \n : " );
		gameLevel = levelCheck(); 		

		// 컴퓨터가 입력한 숫자값  
		while( mySuccLineNum < BINGOSIZE && comSuccLineNum < BINGOSIZE ) 
		{	

			if( playTime >= 1 )
			{				
				System.out.println("\n\n**************************** [ " + playTime + "판째 ] ****");
			}

			++playTime;			

			// 우선순위 테스트 출력용. 
//			checkBingoFan3( comBingFan2, comBingFan3 );
//			printBingoFan( comBingFan3, comBingFan2, 1);			

			// 출력 
			printBingoFan( myBingFan, myBingFan2, 0 );			

			if( playTime >= 2 )
			{
				System.out.println("* 완성된 나의 빙고 수 : " + mySuccLineNum ) ;
			}				

			// 난이도 입력에 따라 컴퓨터 빙고판 다르게 출력하기  
			if( gameLevel == 1 )
				printBingoFan( comBingFan, comBingFan2, 1 );
			else
				printBingoFan( comBingFan, comBingFan2, 2 );			

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

			// 플레이어의 빙고판3을 현재 상황에 맞게 리셋 - 컴퓨터가 참고하기 위해 - 난이도 2(어려움)에서 사용  
			resetBingoFan3( myBingFan3 );
			checkBingoFan3( myBingFan2, myBingFan3 );			

			// 컴퓨터의 빙고판3을 현재 상황에 맞게 리셋 - 컴퓨터가 참고하기 위해 
			resetBingoFan3( comBingFan3 );
			checkBingoFan3( comBingFan2, comBingFan3 );			

			// ** 상대 턴 시작 
			// 우선순위가 높은 빙고 숫자 반환. 
			inputCom = goodBingoNum( comBingFan, comBingFan3, myBingFan); 
			comTurnGood = checkBingoFan2( myBingFan, myBingFan2, inputCom );	////////////////////////////// 이거 AI버전에서 해줘야함!!!! 잊지말기!!!!!!!!!!!!! 별백개 
			checkBingoFan2( comBingFan, comBingFan2, inputCom );	
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
			System.out.println("\n\n!!! --- 비 김 --- !!!\n\n");
		else if( mySuccLineNum == BINGOSIZE )		
			System.out.println("\n\n!!! --- 승 리 --- !!!\n\n");	
		else
			System.out.println("\n\n!!! --- 패 배 --- !!!\n\n");		
	}	

	// 레벨 입력 
	int levelCheck()
	{
		int num = 0 ; 	 		
		Scanner sc = new Scanner( System.in );	

		while( true )
		{	
			// 입력값 자체가 잘못 되었을 경우 ( 문자, 특문, 실수등이 입력될 경우)
			if( !sc.hasNextInt() )			// 여기서 실질적으로 입력 받음 
			{		
				sc.nextLine();				// 잘못된 입력값을 비움. _ hasNext에 남아있던 개행문자를 이용해서 스캐너 비움. 
				System.err.println(" ~ 숫자를 입력해 주세요.");
				continue;
			}
			// 입력값이 일단 숫자. 
			else
			{
				// 숫자+공백+숫자로 이루어졌는지 확인 -------------------
				String tmpStr = sc.nextLine();
				int tmp = 0 ;
				// (공백찾기) 
				for( int i = 0 ; i < tmpStr.length() ; i ++ )
				{
					if( tmpStr.charAt(i) == ' ')
					{						
						tmp++;
						break;
					}					
				}

				if( tmp > 0)
				{
					System.err.println(" ~ 공백없이 입력해 주세요. ");
					continue;
				}
				//-----------------------------------------------				

				num = Integer.parseInt( tmpStr );
			}		

			// 값의 범위 
			if( num != 1 && num != 2 )
			{
				System.err.println(" ~ 1 또는 2 만 입력해 주세요");
				continue;	
			}			
			else
			{				
				break;
			}		
		}		
		return num;			
	}
	// 빙고판3 만들기. 우선순위 설정.  

	void checkBingoFan3( int[][] arr2, int[][] arr3 )
	{	
		boolean dL1 = false;	// 대각선 \ 방향을 확인해야 하는지 알려주는 변수 
		boolean dL2 = false;	// 대각선 / 방향을 확인해야 하는지 알려주는 변수 
		
		int[] numCnt = { 0, 0, 0, 0 };							// 가로, 세로, 대각선\, 대각선/ 의 체크 갯수 
		int[] plusForSameLineBingoCnt = { 0, 10, 15, 25, 35 };	// 같은 줄에 빙고가 몇개 있느냐에 따라 추가 점수를 줌. 실질적으로 2번째 값부터 사용. 
																// ex) 같은 줄에 빙고가 2개 있으면 15*2=30 30의 우선순위값을 더해줌. 		

		for( int i=0 ; i < BINGOSIZE ; i++ )
		{
			for( int j=0 ; j < BINGOSIZE ; j++ )
			{
				// 값이 존재하는 숫자의 가로,세로,대각선(조건이 만족할 경우)의 방향에 위치해 있는 값에 우선순위를 높여줌 
				if( arr2[i][j] == 1 )
				{
					arr3[i][j] = 500 ; 	 // 해당 위치에 값이 체크되어 있음을 의미함. 					

					if( i == j )
						dL1 = true;					

					if( i+j == ( BINGOSIZE-1 ))
						dL2 = true;					

					// 선택된 숫자의 가로, 세로, 대각선(조건이 만족할 경우) 위치에 존재하는 숫자들에게 +10씩 우선순위 높여줌.
					// 선택된 숫자의 가로, 세로, 대각선(조건이 만족할 경우) 위치에 존재하는 빙고된 숫자 갯수 카운트  
					for( int k=0 ; k < BINGOSIZE ; k++ )
					{
						arr3[i][k] += 10;	// 가로  	
						numCnt[0] += cntBingoOfLine( arr2, i, k );	// 선택된 숫자의 가로 줄에 있는 빙고 갯수 카운트 						

						arr3[k][j] += 10;	// 세로  
						numCnt[1] += cntBingoOfLine( arr2, k, j );						

						if( dL1 == true )	// 대각선 \
						{	
							arr3[k][k] += 10;
							numCnt[2] += cntBingoOfLine( arr2, k, k );
						}						

						if( dL2 == true )	// 대각선 /
						{
							arr3[ Math.abs( k - (BINGOSIZE-1)) ][k] += 10;
							numCnt[3] += cntBingoOfLine( arr2, Math.abs( k - (BINGOSIZE-1)), k );
						}
					}					

//					System.out.println("numCnt : " + numCnt[0]  + "\t" + numCnt[1]  + "\t" + numCnt[2]  + "\t" + numCnt[3])	;
//					System.out.println("i : " + i  + "\tj : " + j )	;					

					// 선택된 숫자의 가로, 세로, 대각선(조건이 만족할 경우) 위치에 존재하는 빙고된 숫자 갯수 만큼 우선순위 값 높여줌. 
					if(  numCnt[0] > 1 || numCnt[1] > 1 || numCnt[2] > 1 || numCnt[3] > 1 	)
					{
						for( int k=0 ; k < BINGOSIZE ; k++ )
						{
							// 가로
							if( numCnt[0] > 1 && numCnt[0] < 5 )
								arr3[i][k] += plusForSameLineBingoCnt[ numCnt[0] ];						

							// 세로
							if( numCnt[1] > 1 && numCnt[1] < 5 ) 
								arr3[k][j] += plusForSameLineBingoCnt[ numCnt[1] ];							

							// 대각선 \ 
							if( numCnt[2] > 1 && numCnt[2] < 5 )
								arr3[k][k] += plusForSameLineBingoCnt[ numCnt[2] ];							

							// 대각선 / 
							if( numCnt[3] > 1  && numCnt[3] < 5)
								arr3[ Math.abs( k - (BINGOSIZE-1)) ][k] += plusForSameLineBingoCnt[ numCnt[3] ];				

						}
					}					 

					dL1 = false;
					dL2 = false;
					numCnt[0] = 0 ; numCnt[1] = 0 ; numCnt[2] = 0 ; numCnt[3] = 0 ; 
				}
			}
		}		
	}
	
	// 선택된 줄에 빙고가 존재하면 1을 반환.
	int cntBingoOfLine( int [][] arr2, int i, int j )
	{

		if( arr2[i][j] == 1 )
			return 1;		

		return 0;
	}	

	// 빙고판3에서 제일 높은 우선순위를 가진 숫자 반환. 같은 우선순위를 가진 숫자가 여러개 있을 경우 랜덤으로 반환. 
	int goodBingoNum( int [][] arr1, int [][] arr3, int [][] vsArr1 )
	{

		int maxNum = 0 ;
		ArrayList<Integer> maxNumList = new ArrayList<Integer>() ; 	// 가장 높은 우선순위를 갖는 숫자(빙고판1의 실제 숫자)를 저장. 	

		// 제일 큰 우선순위를 가진 숫자 찾음 
		for( int i=0 ; i < BINGOSIZE ; i++ )
		{
			for( int j=0 ; j < BINGOSIZE ; j++ )
			{
				// 500 미만이고(이미 선택된 숫자가 아니고) 
				if( arr3[i][j] < 500 )
				{
					// 제일 큰 값(maxNum) 보다 큰값이 들어오면 리스트를 비우고 다시 채움. 최대값도 바꿈.
					if(  arr3[i][j] > maxNum )
					{
						maxNum = arr3[i][j] ;
						maxNumList.clear();
						maxNumList.add( arr1[i][j] );
					}
					else if ( arr3[i][j] == maxNum )
					{
						maxNumList.add( arr1[i][j] );
					}
				}
			}
		}

		

		// 리스트 갯수가 2개 이상이면

		if( maxNumList.size() >= 2 )
		{
			// 상대의 빙고판에 없는 숫자면 바로 사용. 
			for( int i = 0 ; i < maxNumList.size() ; i ++ )
			{
				if ( isEqualNumOfArr( vsArr1, maxNumList.get(i)) == false )
					return maxNumList.get(i);
			}		

			// 상대의 빙고판에 있는 숫자면 우선순위 제일 낮은 숫자 사용... 허허 
			System.err.println(" goodBingoNum의 상대의 빙고판에 있는 숫자면 우선순위 제일 낮은 숫자 사용 부분...ㅋㅋ... 이거뜨면 구현하기..");

			// 이거 안해도... 너무 센데... 
			return maxNumList.get(0);		

		}
		else
		{
			return maxNumList.get(0);
		}
	}
}

