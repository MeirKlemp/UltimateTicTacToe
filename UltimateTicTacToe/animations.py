
def millis():
    raise NotImplementedError("You need to implement the millis function to get the time in milliseconds.")

class Animation:

    def __init__(self, duration):
        self.duration = duration
        self.__start_ms = None
        self.__end_ms = None

    def value(self):
        if self.__start_ms is None:
            return 0
        return 1 - (self.__end_ms - min(self.__end_ms, millis())) / self.duration

    def start(self):
        if not self.running():
            self.__start_ms = millis()
            self.__end_ms = self.__start_ms + self.duration
    
    def stop(self):
        pass

    def running(self):
        return self.started() and not self.finished()
    
    def finished(self):
        return millis() >= self.__end_ms

    def started(self):
        return self.__start_ms is not None