# Use official node image as the base image
FROM node:latest as build

# Set the working directory
WORKDIR /usr/src/app

# Add the source code to app
COPY ./ /usr/src/app/

# Install all the dependencies
RUN npm install -g @angular/cli
RUN npm install

CMD ["ng", "serve", "--host", "0.0.0.0"]

# Expose port
EXPOSE 4200