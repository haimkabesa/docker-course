FROM alpine:3.7

CMD ["\nHello Elbit! \
      \n---------------- \
      \nThis text was passed as an argument from the CMD instruction to the ENTRYPOINT instruction.\
      \nYou can override this text at runtime, by passing a different text using the CLI \
      \nin the following way: \
      \n\ndocker container run -it <image name> <new text> \n"]

ENTRYPOINT [ "echo" ]