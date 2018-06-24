FROM alpine:3.6

ARG USER

RUN apk -v --update add \
        python \
        py-pip \
        groff \
        less \
        mailcap \
        && \
    pip install --upgrade awscli==1.14.5 s3cmd==2.0.1 python-magic && \
    apk -v --purge del py-pip && \
    rm /var/cache/apk/* 

RUN adduser -D ${USER}
WORKDIR /home/${USER}

USER ${USER}

CMD [ "help" ]
ENTRYPOINT [ "aws" ]



