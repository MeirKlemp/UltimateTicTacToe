"""
Using PyGame version 2.0.0
"""

import pygame
from gfx import Graphics
pygame.init()

class Window:

    def __init__(self, **kwargs):
        """
        PyGame based window.
        """

        def get_param(param, default):
            return kwargs[param] if param in kwargs else default

        width = get_param("width", 500)
        height = get_param("height", 500)
        self.set_size(width, height, *get_param("flags", (pygame.SCALED,)))

        self.set_title(get_param("title", "PyGame Window"))

        self.clock = pygame.time.Clock()
        self.fps = get_param("fps", 60)

        self.gfx = Graphics(pygame)
        self.gui_objects = get_param("gui_objects", [])
        self.__quit = False

    def start(self):
        while not self.__quit:
            self.clock.tick(self.fps)

            self.keys = pygame.key.get_pressed()

            for event in pygame.event.get():
                if event.type is pygame.QUIT:
                    self.quit()

            self.update()
            self.draw()

    def update(self):
        for go in self.gui_objects:
            go.update(self)

    def draw(self):
        self.win.blit()
        for go in self.gui_objects:
            go.draw(self.gfx)

        pygame.display.uupdate()

    def set_size(self, width, height, *args):
        def get_flag(param, flag):
            return flag if param in args else 0
        flags = get_flag("fullscreen", pygame.FULLSCREEN) | \
            get_flag("resizeable", pygame.RESIZABLE) | \
            get_flag("scaled", pygame.SCALED) | \
            get_flag("double_buffered", pygame.DOUBLEBUF) | \
            get_flag("opengl", pygame.OPENGL) | \
            get_flag("noframe", pygame.NOFRAME) 

        self.win = pygame.display.set_mode((width, height), flags)

    def set_title(self, title):
        pygame.display.set_caption(title)

    def set_fps(self, fps):
        self.fps = fps

    def get_fps(self):
        return self.clock.get_fps()
    
    def quit(self):
        self.__quit = True