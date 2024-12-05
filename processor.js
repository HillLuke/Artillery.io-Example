module.exports = {

  generateRandomUser: (context, events, done) => {
    const randomString = Math.random().toString(36).substring(2, 12);
    context.vars.user = randomString;
    return done();
  }

};