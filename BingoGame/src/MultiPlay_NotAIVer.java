import java.util.*;
import java.io.IOException;

class BingoMulti2
{
	public final static int BINGOSIZE = 5 ;
	
	int[][] myBingFan ;		// 내 빙고판 (숫자저장)
	int[][] myBingFan2 ;	// 내 빙고판 (체크저장)	
	int[][] comBingFan ;	// 컴 빙고판 (숫자저장)
	int[][] comBingFan2 ;	// 컴 빙고판 (체크저장)	
	int mySuccLineNum;		// 내가 성공한 빙고 라인 수 
	int comSuccLineNum;		// 컴이 성공한 빙고 라인 수 
	int playTime;			// 플레이 횟수 
	
	public BingoMulti2()
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
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				myBingFan[i][j] = 0;
				myBingFan2[i][j] = 0;
				comBingFan[i][j] = 0;
				comBingFan2[i][j] = 0;
			}			
		}
		
		// [추가] 
		createBingoFan( myBingFan );
		createBingoFan( comBingFan );
	}
	
	// 빙고판에 중복되지 않는 1~100사이의 숫자들로 채워줌 
	void createBingoFan( int[][] arr )
	{
		Random rd = new Random() ;	// 난수값 생성
		int arrCnt = 0 ;			// 만들어진 빙고 숫자 갯수 저장 
		int tmp = 0; 
		
		// 빙고판 만들기 
		while( arrCnt < BINGOSIZE*BINGOSIZE )
		{			
			tmp = Math.abs(((rd.nextInt()) % 100 ) + 1);  // 1~100 사이의 난수 저장.
			
			// 기존 배열의 값들과 입력값이 같은지 확인. 같은 값이 없으면 false반환.  
			if( isEqualNumOfArr( arr, tmp ) == false )
			{
				initArr( arr, tmp);
				arrCnt++;				
			}		
		}		
	}
	
	
	// num값이 배열에 저장되어 있는 값과 같은지 확인하는 메소드 
	boolean isEqualNumOfArr( int[][] arr, int input )
	{
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				if( arr[i][j] == input )
					return true;				
			}			
		}			
		
		return false;
	}
	
	
	// 빙고판(myBingFan) [초기화용]. 입력된 값을 순서대로 저장.
	void initArr( int[][] arr, int num )
	{
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				if( arr[i][j] == 0 )
				{
					arr[i][j] = num;
					return;
				}				
			}			
		}		
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
	
	
	// 입력값(선택할 빙고 번호)이 유효한 값인지 확인하는 메소드.
	int isValidNum()
	{
		int num = 0 ; 
		System.out.print("* 선택할 번호 : ");		 		
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
			if( num < 1 || num > 100 )
			{
				System.err.println(" ~ 1~100사이의 숫자를 입력해주세요. ");
				continue;
				
			}			
			// 빙고판에 있는 값이면 false를 내보냄
			else if( isEqualNumOfArr( myBingFan, num ) == false )
			{
				System.err.println(" ~ 빙고판에 없는 숫자입니다. 다시입력하세요. ");
				continue;
				
			}
			// 빙고판에 체크된 숫자인지 확인 
			else if( checkBingoFan1( myBingFan, myBingFan2, num ) == true )
			{
				System.err.println(" ~ 이미 선택되어진 숫자입니다. 다른숫자를 선택해 주세요.");
			}
			else
			{				
				break;
			}		
		}
		
		return num;			
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
	
	// 빙고판1에 해당하는 값이 체크되어 있는지(빙고판2를 보고) 확인하는 함수.
	boolean checkBingoFan1( int[][] arr, int[][] arr2, int input )
	{
		for( int i= 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				// 해당값의 빙고판이, 
				if( arr[i][j] == input )
				{
					// 체크 되어 있는 상태면 true반환.
					if( arr2[i][j] == 1 )					
					{	return true; 	}					
					else					
					{	return false;	}					
				}
			}			
		}
		
		return false;
	}
	
	// 완성된 빙고 라인수 카운트 
	int checkSuccLine( int[][] arr )
	{
		int x = 0;						// 가로 빙고 갯수 카운트 
		int[] y = { 0, 0, 0, 0, 0 };	// 세로 빙고 갯수 카운트 
		int[] z = { 0, 0 };				// 대각선 빙고 갯수 카운트 . 앞에꺼 \방향, 뒤에꺼 / 방향 
		int tmpBingoNum = 0;
		
		for( int i = 0 ; i < BINGOSIZE ; i++ )
		{
			for(int j = 0 ; j < BINGOSIZE ; j++)
			{
				// 빙고판이 체크 되어있을 때,   
				if( arr[i][j] == 1 )
				{
					// 가로, 세로, 대각선 빙고 갯수 카운트 변수에 저장
					x++;
					y[j]++;
					
					if( i == j )
						z[0]++;	
					
					if( i+j == (BINGOSIZE-1) )
						z[1]++;		
					
					// 5번째 줄(맨 아랫줄) 일때만 돌음.
					if( i == (BINGOSIZE-1) )
					{
						// 세로 줄 빙고 라인수 확인 
						if( y[j] == BINGOSIZE )			
							tmpBingoNum++;	
						
						// 대각선 줄 빙고 라인수 확인 / 방
						if( i+j == (BINGOSIZE-1) && z[1] == BINGOSIZE ) 
							tmpBingoNum++;
						
						if( i+j == (BINGOSIZE-1)*2 && z[0] == BINGOSIZE ) 
							tmpBingoNum++;
					}
				}
			}
			
			if( x == BINGOSIZE )
				tmpBingoNum++;
			x=0;
		}		
		return tmpBingoNum;		
	}	
}
	
public class MultiPlay_NotAIVer {

	public static void main(String[] args) throws IOException {
		
		BingoMulti2 bingo = new BingoMulti2();
		String inputSt = null;
		
		bingo.play();
		
		// 승리 후
		while( true )
		{ 
			System.out.print("\n\n* 다시 게임을 시작하시겠습니까? (y/n) : ");
			
			while( true )
			{
				Scanner sc = new Scanner(System.in);
				
				inputSt = sc.nextLine();

				if( inputSt.compareTo("y") == 0 ||
					inputSt.compareTo("Y") == 0 ||
					inputSt.compareTo("n") == 0 ||
					inputSt.compareTo("N") == 0
				 )				
				{
					break;
				}
				else
				{				
					System.err.println(" ~ y 또는 n 값만 입력해 주세요.");
				}
			}
			
			if( inputSt.compareTo("n") == 0 || inputSt.compareTo("N") == 0  )
			{
				System.out.println(" ~ 게임종료 ");
				break;
			}
			else if( inputSt.compareTo("y") == 0 || inputSt.compareTo("Y") == 0 )
			{
				bingo.resetBingo();
				bingo.play();
			}			
		}	
		
	}

}
