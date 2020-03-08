# https://mherman.org/blog/dockerizing-an-angular-app/
# base image
FROM node:13.10.1 as build

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY ["Ui/PiControlPanel.Ui.Angular/package.json", "/app/package.json"]
RUN npm install
RUN npm install -g @angular/cli@9.0.5

# add app
COPY ["Ui/PiControlPanel.Ui.Angular", "/app"]

# start app
CMD ng serve --host 0.0.0.0

# healthcheck
HEALTHCHECK --interval=10s --timeout=20s --retries=3 CMD curl --fail http://localhost:4200 || exit 1 