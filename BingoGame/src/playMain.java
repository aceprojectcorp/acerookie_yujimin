import java.util.Scanner;

public class playMain {

	public static void main(String[] args) {
		
		// AI 바보 버전 
//		BingoMulti2 bingo = new BingoMulti2();
		
		// AI 똑똑이 버전 
		MultiPlay_AIVer bingo = new MultiPlay_AIVer();
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
