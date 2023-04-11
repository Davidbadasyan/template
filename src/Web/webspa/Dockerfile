FROM node:18.15.0

WORKDIR /usr/src/webspa
COPY Web/webspa .
RUN npm install

EXPOSE 5173
ENTRYPOINT ["npm", "run", "dev"]