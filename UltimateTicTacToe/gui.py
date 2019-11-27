"""
Using PyGame version 2.0.0
"""

import pygame
import pygame.gfxdraw
from gfx import Graphics
import animations
pygame.init()
animations.millis = pygame.time.get_ticks

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

        self.gui_objects = get_param("gui_objects", [])
        self.__quit = False

    def start(self):
        while not self.__quit:
            self.clock.tick(self.fps)

            self.keys = pygame.key.get_pressed()

            for event in pygame.event.get():
                if event.type is pygame.QUIT:
                    self.quit()
                    
            self.mouse()
            self.update()
            self.draw()

    def mouse(self):
        for i in range(len(self.gui_objects) - 1, -1, -1):
            go = self.gui_objects[i]
            x, y = pygame.mouse.get_pos()

            x -= go.x
            y -= go.y

            if go.in_bounds(x, y) and go.enabled:
                if pygame.mouse.get_pressed()[0]:
                    go.mouse_down(x, y)
                break

    def update(self):
        for go in self.gui_objects:
            go.update(self)

    def draw(self):
        for go in self.gui_objects:
            if go.visible:
                go.draw(Graphics(self.screen.subsurface(pygame.Rect(go.x, go.y, go.width, go.height)), pygame, pygame.gfxdraw))

        pygame.display.update()

    def set_size(self, width, height, *args):
        def get_flag(param, flag):
            return flag if param in args else 0
        flags = get_flag("FULLSCREEN", pygame.FULLSCREEN) | \
            get_flag("RESIZABLE", pygame.RESIZABLE) | \
            get_flag("SCALED", pygame.SCALED) | \
            get_flag("DOUBLE_BUFFERED", pygame.DOUBLEBUF) | \
            get_flag("OPENGL", pygame.OPENGL) | \
            get_flag("NOFRAME", pygame.NOFRAME) 

        self.screen = pygame.display.set_mode((width, height), flags)

    def set_title(self, title):
        pygame.display.set_caption(title)

    def set_fps(self, fps):
        self.fps = fps

    def get_fps(self):
        return self.clock.get_fps()
    
    def quit(self):
        self.__quit = True

class GuiObject:
    def __init__(self, x, y, width, height):
        self.x = x
        self.y = y
        self.width = width
        self.height = height
        self.visible = True
        self.enabled = True

    def in_bounds(self, x, y):
        return 0 <= x - self.x <= self.width and 0 <= y - self.y <= self.height

    def update(self, window):
        pass
    def draw(self, gfx):
        pass
    def mouse_down(self, x, y):
        pass
    def mouse_up(self, x, y):
        pass