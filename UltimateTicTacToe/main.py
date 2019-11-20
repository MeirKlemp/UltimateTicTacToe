from tic_tac_toe import TicTacToe
from gui import Window

def main():
    win = Window(title="TicTacToe", flags=["fullscreen", "scaled"])
    win.start()

if __name__ == "__main__":
    main()
