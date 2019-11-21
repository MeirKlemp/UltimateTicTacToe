from tic_tac_toe import TicTacToe
from gui import Window

def main():
    game = TicTacToe(0, 0, 500, 500, 10, 10)
    win = Window(title="Ultimate Tic Tac Toe", flags=["SCALED", "FULLSCREEN"], gui_objects=[game])
    win.start()

if __name__ == "__main__":
    msg = "Ultimate tic tac toe made by MeirKlemp."
    print('\n{0}\n{1}\n{0}'.format("*" * len(msg), msg))

    main()
