FROM alpine:3.7

WORKDIR /app
RUN touch app-file.txt && \
    echo -e "\nThis file was created by the base-image. \
             \nYou can use this file to hold some metadata about the base-image.\n" > app-file.txt

CMD echo -e "$(cat app-file.txt) \n"

ONBUILD RUN echo -e "\nThe content of this file was overridden by the derived image. \
                     \nYou can use this file to hold some metadata about the derived-image.\n" > app-file.txt